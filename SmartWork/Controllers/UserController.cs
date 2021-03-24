using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Models;
using SmartWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartWork.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public bool RoleController { get; private set; }

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
                return View(_userManager.Users.ToList());
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
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        Name = model.UserName,
                        Surname = model.UserSurname,
                        MiddleName = model.UserMiddleName,
                        PhoneNumber = model.PhoneNumber
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "user");
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
                return View(model);
            }
            else
                return View("../Message/NoRights");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                EditUserViewModel model = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.Name,
                    UserSurname = user.Surname,
                    UserMiddleName = user.MiddleName,
                    PhoneNumber = user.PhoneNumber
                };
                return View(model);
            }
            else
                return View("../Message/NoRights");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if(User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.Name = model.UserName;
                        user.Surname = model.UserSurname;
                        user.MiddleName = model.UserMiddleName;
                        user.PhoneNumber = model.PhoneNumber;

                        var result = await _userManager.UpdateAsync(user);
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
                } 
                return View(model);
            }
            else
                return View("../Message/NoRights");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            if(User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
                return RedirectToAction("Index");
            }
            else
                return View("../Message/NoRights");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            if(User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
                return View(model);
            }
            else
                return View("../Message/NoRights");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(User.Claims.ToList().Where(r => r.Value.Equals("admin")).Any())
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        IdentityResult result =
                            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User is not found");
                    }
                }
                return View(model);
            }
            else
                return View("../Message/NoRights");  
        }
    }
}
