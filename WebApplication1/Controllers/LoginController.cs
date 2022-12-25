using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Razor.Language;
using RazorEngine;
using RazorEngine.Templating;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Service;

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

                        return LocalRedirect("/Campaigns");
                    }
                    else
                    {
                        if (resultOfLogin.IsLockedOut)
                        {
                            ModelState.AddModelError(string.Empty, "Konto użytkownika jest zablokowane.");
                            return View("ErrorLogin", model);
                        }
                        ModelState.AddModelError(string.Empty, "Nieudana próba logowania.");
                        return View("ErrorLogin", model);
                    }
                }
            }
            return View("Index");
        }
    }
}
