using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications.UserSpecification;
using SmartWork.Core.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync(UserSpecification<User> specification);
        Task<UserInfoViewModel> GetUserProfileAsync(ClaimsPrincipal user);
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<Subscribe>> GetUserSubscribesAsync(string id);
        Task<IActionResult> SignInAsync(LoginViewModel model);
        Task<IdentityResult> SingUpAsync(CreateUserViewModel model);
        Task<ActionResult> Logout();
        Task<IdentityResult> UpdateAsync(EditUserViewModel model);
        Task<IdentityResult> RemoveAsync(string id);
    }
}
