using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using Web.ViewModels.AccountManagementViewModels;

namespace Web.Controllers.AccountManagementControllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly PulseContext _pulseContext;

        public ManageController(UserManager<Account> userManager, SignInManager<Account> signInManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _pulseContext = pulseContext;
        }

        [Route("Account/Manage")]
        public async Task<IActionResult> Manage()
        {
            var account = await _userManager.GetUserAsync(User);
            var user = _pulseContext.User.Where(u => u.AccountId == account.Id).ToList()[0];

            ManageViewModel model = new ManageViewModel
            {
                Email = account.Email,
                UserName = account.UserName,
                Name = user.Name,
                PhoneNumber = account.PhoneNumber,
                Location = user.Location
            };

            return View("Views/AccountManagement/Manage.cshtml", model);
        }

        [HttpPost()]
        [Route("Account/Manage")]
        public async Task<IActionResult> ChangePassword(ManageViewModel input)
        {
            var account = await _userManager.GetUserAsync(User);
            var user = _pulseContext.User.Where(u => u.AccountId == account.Id).ToList()[0];

            ManageViewModel model = new ManageViewModel
            {
                Email = account.Email,
                UserName = account.UserName,
                Name = user.Name,
                PhoneNumber = account.PhoneNumber,
                Location = user.Location
            };
            
            if (ModelState.IsValid)
            {
                var passwordResult = await _userManager.ChangePasswordAsync(account, input.Password, input.NewPassword);
                
                if(!passwordResult.Succeeded){
                    foreach(var error in passwordResult.Errors){
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                var result = await _userManager.UpdateAsync(account);
                if (result.Succeeded)
                    {
                        await _pulseContext.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
            }

            return View("Views/AccountManagement/Manage.cshtml", model);
        }
    }
}