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
    public class MachinesController : ControllerBase
    {
        private readonly SomaticContext _context;

        public MachinesController(SomaticContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MachineSearchViewModel>>> Get(string? term = null, int? page = null, int? itemsPerPage = null)
        {
            var query = _context.Machines.Include(e => e.MediaUrls).AsQueryable();
            if (term is not null)
                query = query.Where(e => e.Name.ToLower().Contains(term)).ApplyPagination(page, itemsPerPage);
            List<MachineSearchViewModel> data =
                await query
                .Select(e => new MachineSearchViewModel(e.Id, e.Name, e.Number, e.MediaUrls.Select(e => e.Url).ToList()))
                .ToListAsync();
            return data;
        }
        [HttpPost]
        public async Task<ActionResult> Post(MachineCreateViewModel model)
        {
            var urls = model.MediaUrls.Select(url => new MediaUrl(url)).ToList();
            var entity = new Machine(model.Number, model.Name, urls);
            await _context.Machines.AddAsync(entity);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(MachineCreateViewModel model, int id)
        {
            try
            {
                var machine = await _context.Machines.Include(e => e.MediaUrls).FirstOrDefaultAsync(e => e.Id == id);
                if (machine is null) return BadRequest("Machine invalid");
                var urls = model.MediaUrls.Select(url => new MediaUrl(url)).ToList();
                machine.UpdateAll(new Machine(model.Number, model.Name, urls));
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
                var machine = await _context.Machines.Include(e=> e.MediaUrls).FirstOrDefaultAsync(e=> e.Id == id);
                if (machine is null)
                    return BadRequest("Invalid machines");
                _context.Machines.Remove(machine);
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
