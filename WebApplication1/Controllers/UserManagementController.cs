using System;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models.UserManagement;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManagement;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManagement = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            var AllRoles = _roleManager.Roles.ToList();
            var userRole = new SelectList(AllRoles, "Name", "Name");

            ViewBag.Roles = userRole;
            return View();
        }

        [HttpGet("/UserManagement/EditUser/{guid}")]
        public async Task<IActionResult> EditUser(string guid)
        {
            var user = await _userManagement.FindByIdAsync(guid);

            EditUserModel model = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = guid
            };

            var AllRoles = _roleManager.Roles.ToList();
            var UserRoles = await _userManagement.GetRolesAsync(user);
            var userRole = new SelectList(AllRoles, "Name", "Name", UserRoles.FirstOrDefault());

            ViewBag.Roles = userRole;

            return View("EditUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModel model)
        {
            var user = await _userManagement.FindByIdAsync(model.Id);

            var token = await _userManagement.GenerateChangeEmailTokenAsync(user, model.Email);


            await _userManagement.ChangeEmailAsync(user, model.Email, token);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            user.UserName = model.Email;
            await _userManagement.UpdateNormalizedEmailAsync(user);
            await _userManagement.UpdateNormalizedUserNameAsync(user);

            await _userManagement.UpdateAsync(user);

            await _userManagement.RemoveFromRolesAsync(user, await _userManagement.GetRolesAsync(user));

            await _userManagement.AddToRoleAsync(user, model.Role);

            return RedirectToAction(nameof(ShowAllUsers));
        }

        [HttpGet("/UserManagement/Block/{guid}")]
        public async Task<IActionResult> Block(string guid)
        {
            var user = await _userManagement.FindByIdAsync(guid);

            await _userManagement.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            return RedirectToAction("ShowAllUsers");
        }

        [HttpGet("/UserManagement/Unlock/{guid}")]
        public async Task<IActionResult> Unlock(string guid)
        {
            var user = await _userManagement.FindByIdAsync(guid);

            await _userManagement.SetLockoutEndDateAsync(user, DateTimeOffset.Now);

            return RedirectToAction("ShowAllUsers");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserAddView model)
        {

            var AllRoles = _roleManager.Roles.ToList();
            var userRole = new SelectList(AllRoles, "Name", "Name");

            ViewBag.Roles = userRole;

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email
                };
                
                var result = await _userManagement.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var newUser = await _userManagement.FindByEmailAsync(model.Email);
                    await _userManagement.AddToRoleAsync(newUser, model.Role);

                    return RedirectToAction("ShowAllUsers", "UserManagement");
                }
                else
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);

                        return View();
                    }

                    
                }
            }

            ModelState.AddModelError("", "błąd");
            return View();
        }

        /* public IActionResult ShowAllRoles()
         {
             return View(_roleManager.Roles);
         }*/

        public async Task<IActionResult> ShowAllUsers()
        {
            var model = new List<ShowUsersWithRole>();
            foreach (var user in _userManagement.Users)
            {
                var UserInRole = new ShowUsersWithRole()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    LockoutEnd = user.LockoutEnd
                };

                model.Add(UserInRole);
            }

            foreach (var user in model)
            {
                var UserFromUserManagement = await _userManagement.FindByIdAsync(user.Id);
                var Roles = await _userManagement.GetRolesAsync(UserFromUserManagement);

                user.Role = Roles.FirstOrDefault();
            }

            return View(model);
        }

        [HttpGet("/UserManagement/Delete/{guid}")]
        public async Task<IActionResult> Delete(string guid)
        {

            var user = await _userManagement.FindByIdAsync(guid);

            var result = await _userManagement.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ShowAllUsers");
            }

            return BadRequest();
        }

        [HttpGet("/UserManagement/SendResetPassword/{guid}")]
        public async Task<IActionResult> SendResetPassword(string guid)
        {
            var user = await _userManagement.FindByIdAsync(guid);

            var code = await _userManagement.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Login",
                new { UserId = user.Id, code = code }, "https");

            EmailService emailService = new EmailService("serwer1311887.home.pl", 465, SecureSocketOptions.SslOnConnect);

            emailService.SendAsync("d.parol@business-care.pl", user.Email, "Resetowanie hasła", "Twoje hasło zostało zrestartowane. <a href=\"" + callbackUrl + "\">Kliknij tu aby je zrestartować</a>");


            return RedirectToAction("ShowAllUsers");
        }
    }
}
