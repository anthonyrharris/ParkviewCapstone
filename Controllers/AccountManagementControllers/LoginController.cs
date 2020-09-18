using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.Models;
using Web.ViewModels.AccountManagementViewModels;

namespace Web.Controllers.AccountManagementControllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<Account> _signInManager;

        public LoginController(SignInManager<Account> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("Account/Login")]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Views/AccountManagement/Login.cshtml");
        }

        [Route("Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            
            return View("Views/AccountManagement/Login.cshtml");
        }
    }
}