using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using SmartWork.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userManager.Users.ToListAsync();
        }

        // GET api/users/5
        [Route("GetUserById")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // GET api/users/GetUserSubscribes/5
        [HttpGet("GetUserSubscribes/{id}")]
        public async Task<ActionResult<User>> GetUserSubscribes(string id)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user == null)
                return NotFound();
            List<Subscribe> subscribes = await db.Subscribe.Where(s => s.UserId == id).ToListAsync();
            return new ObjectResult(subscribes);
        }

        // POST api/users/register
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<User>> UserRegister(CreateUserViewModel model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Patronymic = model.Patronymic,
                PhoneNumber = model.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "user");
                return Ok(result);
            }
            catch
            {
                throw;
            }               
        }

        // POST api/users/update
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<User>> UserUpdate(EditUserViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            user.Patronymic = model.Patronymic;
            user.PhoneNumber = model.PhoneNumber;

            try
            {
                var result = await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return Ok(user);
        }
    }
}
