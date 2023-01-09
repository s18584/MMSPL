﻿using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_signInManager.Context.User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Campaigns");
            }
            return View();
        }

        public IActionResult ErrorLogin(LoginViewModel model)
        {
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> OnPost(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var singOutResult = _signInManager.SignOutAsync();
                if (singOutResult.IsCompletedSuccessfully)
                {
                    var resultOfLogin = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                    if (resultOfLogin.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        Serilog.Log.Information("User zalogowany porpawnie: " + user.Email);
                        if (await _userManager.IsInRoleAsync(user, "RODO"))
                        {
                            return LocalRedirect("/Customers");
                        }
                        return LocalRedirect("/Campaigns");
                    }
                    else
                    {
                        if (resultOfLogin.IsLockedOut)
                        {
                            Serilog.Log.Information("Zablokowany user chciał się zalogować: " + model.Email);
                            ModelState.AddModelError(string.Empty, "Konto użytkownika jest zablokowane.");
                            return View("Index", model);
                        }

                        Serilog.Log.Information("Nieudana próba zalogowania: " + model.Email);
                        ModelState.AddModelError(string.Empty, "Nieudana próba logowania.");
                        return View("Index", model);

                    }
                }
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string UserId, string code)
        {
            if (UserId.IsNullOrEmpty() || code.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(UserId);
            var model = new ResetPasswordModel
            {
                Email = user.Email,
                ResetPasswordToken = code
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (!model.Password.Equals(model.RepeatedPassword))
            {
                ModelState.AddModelError(string.Empty, "Podane hasła różnią się!");
                return View("ResetPassword", model);
            }


            await _userManager.ResetPasswordAsync(user, model.ResetPasswordToken, model.Password);
            await _userManager.ResetAccessFailedCountAsync(user);
            await _userManager.SetLockoutEndDateAsync(user, null);

            ModelState.AddModelError(string.Empty, "Hasło zostało zmienione!");
            return View("Index");

        }

        public IActionResult SingOff()
        {
            if (_signInManager.IsSignedIn(_signInManager.Context.User))
            {
                var singOutResult = _signInManager.SignOutAsync();
                if (singOutResult.IsCompletedSuccessfully)
                {
                    return RedirectToAction("index", "Login");
                }

            }

            return BadRequest();
        }
    }
}
