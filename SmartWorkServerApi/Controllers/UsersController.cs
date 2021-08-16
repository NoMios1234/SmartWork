using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications.UserSpecification;
using SmartWork.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsersAsync(new UserSpecification<User>(u => u.Id != ""));
        }

        // GET: api/Users/Profile
        [HttpGet("Profile")]
        public Task<UserInfoViewModel> GetUserProfile()
        {
            return _userService.GetUserProfileAsync(this.User);
        }

        // POST: api/Users/5
        [HttpPost("{id}")]
        public Task<User> GetUserById(string id)
        {
            return _userService.GetUserByIdAsync(id);
        }

        // POST: api/Users/Subscribes/5
        [HttpPost("Subscribes/{id}")]
        public Task<IEnumerable<Subscribe>> GetUserSubscribes(string id)
        {
            return _userService.GetUserSubscribesAsync(id);
        }

        // POST: api/Users/SignIn
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            return await _userService.SignInAsync(model);
        }

        // POST: api/Users/SingUp
        [HttpPost("SingUp")]
        public Task<IdentityResult> SingUp(CreateUserViewModel model)
        {
            return _userService.SingUpAsync(model);
        }

        // POST: api/Users/Logout
        [HttpPost("Logout")]
        public Task<ActionResult> Logout()
        {
            return _userService.Logout();
        }

        // PUT: api/Users/Update
        [HttpPut("Update")]
        public Task<IdentityResult> Update(EditUserViewModel model)
        {
            return _userService.UpdateAsync(model);
        }

        // DELETE: api/Users/Remove/5
        [HttpDelete("Remove/{id}")]
        public Task<IdentityResult> Remove(string id)
        {
            return _userService.RemoveAsync(id);
        } 
    }
}