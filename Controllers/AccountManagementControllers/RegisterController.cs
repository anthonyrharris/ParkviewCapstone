using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using System;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers.AccountManagementControllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly PulseContext _pulseContext;

        public RegisterController(UserManager<Account> userManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _pulseContext = pulseContext;
        }

        [Route("Account/Register")]
        public IActionResult Register()
        {
            return View("Views/AccountManagement/Register.cshtml");
        }

        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account
                {
                    Email = input.Email,
                    UserName = input.UserName,
                    PhoneNumber = input.PhoneNumber,
                    Active = true,
                    DateJoined = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(account, input.Password);
                if (result.Succeeded)
                {
                    _pulseContext.User.Add(new User
                    {
                        Location = input.Location,
                        Name = input.Name,
                        Id = Guid.NewGuid().ToString(),
                        AccountId = account.Id
                    });

                    await _pulseContext.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(account, "Patient");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/AccountManagement/Register.cshtml");
        }
    }
}
