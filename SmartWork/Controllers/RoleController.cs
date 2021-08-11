using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using SmartWork.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
                return View(_roleManager.Roles.ToList());
            else
                return View("../Message/NoRights");
        }
        
        public IActionResult Create()
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
                return View();
            else
                return View("../Message/NoRights");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                if (!string.IsNullOrEmpty(name))
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(name);
            }
            else
                return View("../Message/NoRights");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            else
                return View("../Message/NoRights");
        }

        public IActionResult UserList()
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                return View(_userManager.Users.ToList());
            }
            else
                return View("../Message/NoRights");
        }
            

        public async Task<IActionResult> Edit(string userId)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };
                    return View(model);
                }

                return NotFound();
            }
            else
                return View("../Message/NoRights");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return RedirectToAction("UserList");
                }
                return NotFound();
            }
            else
                return View("../Message/NoRights");
        }
    }
}
