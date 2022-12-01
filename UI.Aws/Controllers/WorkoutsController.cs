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
    public class WorkoutsController : ControllerBase
    {
        private readonly SomaticContext _context;

        public WorkoutsController(SomaticContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<WorkoutSearchViewModel>>> Get(int? leanerId = null,string? term = null, int? page = null, int? itemsPerPage = null)
        {
            var query = _context.Workouts.AsQueryable();
            if (term is not null)
                query = query.Where(e => e.Name.ToLower().Contains(term));
            if (leanerId is not null)
                query = query.Where(e => e.Leaners.Any(e => e.Id == leanerId));

            var data = await query.ApplyPagination(page, itemsPerPage).Include(e => e.CreatedBy).Include(e => e.Leaners).ToListAsync();
            var models = data.Select(e =>
            {
                var leanersModel = e.Leaners.Select(l => new WorkoutLeanerSearchViewModel(l.Name, l.ProfilePictureUrl, l.Id)).ToList();
                return new WorkoutSearchViewModel(e.Name, e.CreatedBy.Name, e.CreatedBy.ProfilePictureUrl, leanersModel, e.Id);
            });
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WorkoutViewModel>> Get(int id)
        {
            var workout = await _context.Workouts
                .Include(e => e.CreatedBy)
                .Include(e => e.Leaners)
                .Include(e => e.Sequences)
                    .ThenInclude(e => e.Exercise)
                        .ThenInclude(e => e.Machine)
                .Include(e => e.Sequences)
                    .ThenInclude(e => e.Exercise)
                        .ThenInclude(e => e.MediaUrls)
                 .Include(e => e.Sequences)
                    .ThenInclude(e => e.Exercise)
                        .ThenInclude(e => e.Machine)
                            .ThenInclude(e => e.MediaUrls)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (workout is null)
                throw new Exception("Invalid workout");
            var sequences = new List<SequenceViewModel>();
            workout.Sequences.OrderBy(e => e.Order).ToList().ForEach(s =>
            {
                var exerciseMediaUrls = s.Exercise.MediaUrls.Select(e => e.Url).ToList();
                var machineMediaUrls = s.Exercise.Machine.MediaUrls.Select(e => e.Url).ToList();
                exerciseMediaUrls.AddRange(machineMediaUrls);
                sequences.Add(new(
                    s.Exercise.Id,
                    s.Series,
                    s.Repetitions,
                    s.UntilFail,
                    s.Exercise.Name,
                    s.Exercise.Machine.Name,
                    s.Exercise.Machine.Number,
                    exerciseMediaUrls));
            });
            List<WorkoutLeanerSearchViewModel> leaners = workout.Leaners.Select(e => new WorkoutLeanerSearchViewModel(e.Name, e.ProfilePictureUrl, e.Id)).ToList();
            var model = new WorkoutViewModel(workout.Id, workout.Name, workout.CreatedBy.Name, workout.CreatedBy.ProfilePictureUrl, sequences, leaners,workout.Description);
            return Ok(model);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(WorkoutCreateViewModel model, int id)
        {
            try
            {
                var workout = await _context.Workouts.Include(e=> e.Leaners).Include(e=> e.Sequences).FirstOrDefaultAsync(e => e.Id == id);
                if (workout is null)
                    return BadRequest("Invalid workout");

                var assignedUser = await _context.Teachers.FindAsync(model.UserId);
                if (assignedUser is null)
                    throw new Exception("Invalid assigned user to workout");

                var sequences = GetSequences(model);
                var leaners = await _context.Leaners.Where(e => model.LeanersIds.Contains(e.Id)).ToListAsync();
                workout.UpdateAllWorkout(model.Name, model.Description, assignedUser, sequences, leaners);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<Sequence> GetSequences(WorkoutCreateViewModel model)
        {
            int orderOfSequence = 0;
            return model.Sequences.Select(m =>
            {
                var exercise = _context.Exercises.Find(m.Id);
                if (exercise is null)
                    throw new Exception($"Invalid exercise on sequence {m.Id}");
                orderOfSequence++;
                return new Sequence(exercise, m.Series, m.Repetitions, m.UntilFail, orderOfSequence);
            }).ToList();
        }

        [HttpPost]
        public async Task<ActionResult> Post(WorkoutCreateViewModel model)
        {
            var leaners = await _context.Leaners.Where(e => model.LeanersIds.Contains(e.Id)).ToListAsync();
            int orderOfSequence = 0;
            var sequences = model.Sequences.Select(m =>
            {
                var exercise = _context.Exercises.Find(m.Id);
                if (exercise is null)
                    throw new Exception($"Invalid exercise on sequence {m.Id}");
                orderOfSequence++;
                return new Sequence(exercise, m.Series, m.Repetitions, m.UntilFail, orderOfSequence);
            }).ToList();
            var assignedUser = await _context.Teachers.FindAsync(model.UserId);
            if (assignedUser is null)
                throw new Exception("Invalid assigned user to workout");
            var entity = new Workout(model.Name, model.Description, assignedUser,sequences,leaners);
            await _context.Workouts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var workout = await _context.Workouts.Include(e => e.Leaners).Include(e=> e.Sequences).FirstOrDefaultAsync(e => e.Id == id);
                if (workout is null)
                    throw new Exception("Invalid workout");
                _context.Remove(workout);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
