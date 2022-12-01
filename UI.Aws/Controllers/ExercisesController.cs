using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Aws.Utils;
using UI.Aws.ViewModels;

namespace UI.Aws.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly SomaticContext _context;

        public ExercisesController(SomaticContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ExerciseSearchViewModel>>> Get(string? term = null, int? page = null, int? itemsPerPage = null)
        {
            try
            {
                var query = _context.Exercises.Include(e => e.CreatedByUser).Include(e => e.MediaUrls).AsQueryable();
                if (term is not null)
                    query = query.Where(e => e.Name.ToLower().Contains(term)).ApplyPagination(page, itemsPerPage);
                List<ExerciseSearchViewModel> data =
                    await query.Include(e => e.Machine)
                    .Select(e => new ExerciseSearchViewModel(e.Id, e.Name, e.Machine.Name, e.Machine.Number, e.CreatedByUser.Name, e.CreatedByUser.ProfilePictureUrl, e.Machine.Id, e.MediaUrls.Select(e => e.Url).ToList())
                    )
                    .ToListAsync();
                return data;
            }
            catch (Exception e)
            {
                if (e.InnerException is not null && e.InnerException.Message is not null)
                    return BadRequest(e.InnerException.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post(ExerciseCreateViewModel model)
        {
            var urls = model.MediaUrls.Select(url => new MediaUrl(url)).ToList();
            var user = await _context.Users.FindAsync(model.UserId);
            var machine = await _context.Machines.FindAsync(model.MachineId);
            if (user is null || machine is null) return BadRequest("Machine invalid");
            var entity = new Exercise(model.Name, user, machine, urls);
            await _context.Exercises.AddAsync(entity);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(ExerciseCreateViewModel model, int id)
        {
            try
            {
                var exercise = await _context.Exercises.Include(e => e.MediaUrls).FirstOrDefaultAsync(e => e.Id == id);
                if (exercise is null) return BadRequest("Exercise invalid");
                var urls = model.MediaUrls.Select(url => new MediaUrl(url)).ToList();
                var user = await _context.Users.FindAsync(model.UserId);
                var machine = await _context.Machines.FindAsync(model.MachineId);
                if (user is null || machine is null) return BadRequest("Machine or user invalid");
                exercise.UpdateAll(new Exercise(model.Name, user, machine, urls));
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exercise = await _context.Exercises.FindAsync(id);
                if (exercise is null)
                    return BadRequest("Invalid exercise");
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
