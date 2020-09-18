using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;

namespace Web.Controllers.UserManagementControllers
{
    [Authorize(Roles = "Admin")]
    public class RemoveUserController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly PulseContext _pulseContext;

        public RemoveUserController(UserManager<Account> userManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _pulseContext = pulseContext;
        }

        [HttpPost()]
        [Route("Users/Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            var account = await _userManager.FindByIdAsync(Id);
            var user = _pulseContext.User.Where(u => u.AccountId == Id).ToList()[0];

            var messages = _pulseContext.Message.Where(m => m.ReceiverId == user.Id || m.SenderId == user.Id).ToList();
            foreach (var message in messages)
            {
                _pulseContext.Remove(message);
            }

            if (_userManager.GetRolesAsync(account).Result.Contains("PeerCoach"))
            {
                var assignedPatients = _pulseContext.Assignment.Where(a => a.PeerCoachId == user.Id).ToList();
                foreach (var patient in assignedPatients)
                {
                    _pulseContext.Remove(patient);
                }
            }
            else if (_userManager.GetRolesAsync(account).Result.Contains("Patient"))
            {
                var assignedPeerCoach = _pulseContext.Assignment.Where(a => a.PatientId == user.Id).ToList();
                foreach (var coach in assignedPeerCoach)
                {
                    _pulseContext.Remove(coach);
                }
            }

            await _pulseContext.SaveChangesAsync();
            _pulseContext.Remove(user);
            await _pulseContext.SaveChangesAsync();
            var result = await _userManager.DeleteAsync(account);
            return RedirectToAction("Index", "Index");
        }
    }
}