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
    public class UsersController : ControllerBase
    {
        private readonly SomaticContext _context;

        public UsersController(SomaticContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<List<UserSearchViewModel>>> Login(LoginModel model)
        {
            var leaner = await _context.Leaners.FirstOrDefaultAsync(e => e.UserNick == model.User && e.Pass == model.Pass);
            var teacher = await _context.Teachers.FirstOrDefaultAsync(e => e.UserNick == model.User && e.Pass == model.Pass);
            if (leaner is null && teacher is null)
                return Ok(new { Auth = false });
            return Ok(new
            {
                Auth = true,
                Role = leaner != null ? "Leaner" : "Teacher",
                Id = leaner != null ? leaner.Id : teacher.Id
            });
        }

        [HttpGet]
        [Route("leaners")]
        public async Task<ActionResult<List<UserSearchViewModel>>> Get(string? term = null, int? page = null, int? itemsPerPage = null)
        {
            var query = _context.Leaners.AsQueryable();
            if (term is not null)
                query = query.Where(e => e.Name.ToLower().Contains(term)).ApplyPagination(page, itemsPerPage);
            List<UserSearchViewModel> data =
                await query
                .Select(e => new UserSearchViewModel(e.Id, e.Name, e.UserNick, e.Pass))
                .ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        [Route("leaners")]
        public async Task<ActionResult> SignUp(UserSignupViewModel model)
        {
            var newUser = new Leaner(model.Name, model.User, model.Pass, "");
            await _context.Leaners.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpGet]
        [Route("teachers")]
        public async Task<ActionResult<List<UserSearchViewModel>>> GetTeachers(string? term = null, int? page = null, int? itemsPerPage = null)
        {
            var query = _context.Teachers.AsQueryable();
            if (term is not null)
                query = query.Where(e => e.Name.ToLower().Contains(term)).ApplyPagination(page, itemsPerPage);
            List<UserSearchViewModel> data =
                await query
                .Select(e => new UserSearchViewModel(e.Id, e.Name, e.UserNick, e.Pass))
                .ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        [Route("teachers")]
        public async Task<ActionResult> SignUpTeacher(UserSignupViewModel model)
        {
            var newUser = new Teacher(model.Name, model.User, model.Pass, "");
            await _context.Teachers.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
    public class LoginModel
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }

}
