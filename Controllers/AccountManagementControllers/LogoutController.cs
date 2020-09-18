using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.Models;
using System.Threading.Tasks;

namespace Web.Views.AccountManagement
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<Account> _signInManager;

        public LogoutController(SignInManager<Account> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
