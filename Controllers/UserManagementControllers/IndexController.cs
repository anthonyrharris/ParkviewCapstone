using System.Collections.Generic;
using System.Linq;
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
    public class IndexController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly PulseContext _pulseContext;

        public IndexController(UserManager<Account> userManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _pulseContext = pulseContext;
        }

        [Route("Users")]
        public async Task<IActionResult> Index()
        {
            var allUsers = _pulseContext.User.ToList();
            var allAccounts = _userManager.Users.ToList();
            var users = new List<UserViewModel>();

            foreach(var user in allUsers){
                var account = await _userManager.FindByIdAsync(user.AccountId);

                users.Add(new UserViewModel
                {
                    Id = account.Id,
                    Name = user.Name,
                    UserName = account.UserName,
                    Role = (await _userManager.GetRolesAsync(account))[0].ToString()
            });
            }

            return View("Views/UserManagement/Index.cshtml", users);
        }
    }
}