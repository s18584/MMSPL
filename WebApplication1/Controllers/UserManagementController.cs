using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.models.databasemodels;
using WebApplication1.Models.UserManagement;

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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet("/UserManagement/EditUser/{guid}")]
        public async Task<IActionResult> EditUser(string guid)
        {
            var user = await _userManagement.FindByIdAsync(guid);

            EditUserModel model = new EditUserModel();

            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Id = guid;
            
            
            return View("EditUser",model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModel model)
        {
            var user = await _userManagement.FindByIdAsync(model.Id);
            
            var token =await _userManagement.GenerateChangeEmailTokenAsync(user, model.Email);

            
            await _userManagement.ChangeEmailAsync(user, model.Email, token);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            
            user.UserName = model.Email;
            await _userManagement.UpdateNormalizedEmailAsync(user);
            await _userManagement.UpdateNormalizedUserNameAsync(user);

            await _userManagement.UpdateAsync(user);


            return RedirectToAction(nameof(ShowAllUsers));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserAddView model)
        {
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                }
            }

            return RedirectToAction();

        }

        public IActionResult ShowAllRoles()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult ShowAllUsers()
        {
            return View(_userManagement.Users);
        }

        
    }
}
