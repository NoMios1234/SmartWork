using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartWork.Core.Entities;
using SmartWork.Core.ViewModels;
using SmartWork.Data.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;


        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userManager.Users.ToListAsync();
        }

        // GET users/5
        [HttpGet("GetUserById/{id}")]
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
            var user = new User 
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
            catch(Exception ex)
            {
                return BadRequest(ex);
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
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                return Ok(user);
            }
            return BadRequest();
        }

        // POST api/users/Login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }

        //GET : /api/users/Profile
        [Route("Profile")]
        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Email,
                user.FirstName,
                user.SecondName,
                user.Patronymic,
                user.PhoneNumber
            };
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<ObjectResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return new ObjectResult(true);
        }
    }
}