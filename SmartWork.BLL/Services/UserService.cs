using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Models;
using SmartWork.Core.Specifications.UserSpecification;
using SmartWork.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartWork.BLL.Services
{
    public class UserService : IUserService
    {
        // READONLY 
        private readonly IUserRepository<User> _repository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly ILogger<UserService> _logger;
        private readonly ISubscribeService _subscribeService;

        public UserService(IUserRepository<User> repository, ILogger<UserService> logger,
            ISubscribeService subscribeService)
        {
            _repository = repository;
            _logger = logger;
            _subscribeService = subscribeService;
        }
        
        // GET Users
        public Task<IEnumerable<User>> GetUsersAsync(UserSpecification<User> specification)
        {
            return _repository.GetAsync(specification);
        }

        // GET UserRoles
        public async Task<IEnumerable<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.GetRolesAsync(user);
        }

        // GET Logged In User
        public Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            return _userManager.FindByIdAsync(user.Claims.First(c => c.Type == "UserId").Value);
        }

        // GET Logged In UserProfile
        public async Task<UserInfoViewModel> GetUserProfileAsync(ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserId").Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            return JsonConvert.DeserializeObject<UserInfoViewModel>(
                JsonConvert.SerializeObject(currentUser));
        }

        // GET UserById
        public Task<User> GetUserByIdAsync(string id)
        {
            return _repository.FindAsync(id);
        }

        // GET UserSubscribes by Id
        public Task<IEnumerable<Subscribe>> GetUserSubscribesAsync(string id)
        {
            return _subscribeService.UserSubscribesAsync(id);
        }

        // ADD UserRole
        public Task<IdentityResult> AddUserRoleAsync(User user, string role)
        {
            return _userManager.AddToRoleAsync(user, role);
        }

        // ADD UserRoles
        public Task<IdentityResult> AddUserRolesAsync(User user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(user, roles);
        }

        // SIGN IN
        public async Task<IActionResult> SignInAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Roles",  string.Join(",", await _userManager.GetRolesAsync(user)))
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return new OkObjectResult(new { token });
            }
            else
                return new BadRequestObjectResult(new { message = "Username or password is incorrect." });
        }

        // SIGN UP
        public async Task<IdentityResult> SingUpAsync(CreateUserViewModel model)
        {
            const string userRole = "user";
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                Patronymic = model.Patronymic
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRole);
                await _signInManager.SignInAsync(user, false);
            }
            return result;
        }

        // LOGOUT
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService/LogoutAsync\n" + ex.Message);
                return new BadRequestResult();
            }
        }

        // UPDATE 
        public async Task<IdentityResult> UpdateAsync(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.UserName = model.Email;
            user.Patronymic = model.Patronymic;
            user.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }

        // CHANGE Password
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            return result;
        }

        // REMOVE
        public async Task<IdentityResult> RemoveAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(user); ;
        }

        // REMOVE UserRole
        public async Task<IdentityResult> RemoveUserRoleAsync(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
