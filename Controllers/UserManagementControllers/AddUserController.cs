using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using Pulse.ViewModels.UserManagementViewModels;

namespace Web.Controllers.UserManagementControllers
{
    [Authorize]
    public class AddUserController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PulseContext _pulseContext;

        public AddUserController(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _pulseContext = pulseContext;
        }

        [Route("Users/Add")]
        public IActionResult Create(String role)
        {
            var model = new CreateUserViewModel
            {
                Role = role
            };
            return View("Views/UserManagement/Create.cshtml", model);
        }

        [HttpPost]
        [Route("Users/Add")]
        public async Task<IActionResult> Create(CreateUserViewModel input)
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

                var result = await _userManager.CreateAsync(account, GeneratePassword());
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

                    await _userManager.AddToRoleAsync(account, input.Role);
                    return RedirectToAction("Index", "Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/UserManagement/Create.cshtml", input);
        }

        private string GeneratePassword()
        {
            return "P@ssword12345";
        }
    }
}