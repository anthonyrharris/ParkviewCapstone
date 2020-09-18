using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using Web.ViewModels.UserManagementViewModels;

namespace Web.Controllers.UserManagementControllers
{
    [Authorize(Roles = "Admin")]
    public class EditUserController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly PulseContext _pulseContext;

        public EditUserController(UserManager<Account> userManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _pulseContext = pulseContext;
        }

        [Route("Users/Edit")]
        public async Task<IActionResult> Edit(string Id)
        {
            var account = await _userManager.FindByIdAsync(Id);
            var user = _pulseContext.User.Where(u => u.AccountId == Id).ToList()[0];

            var model = new EditUserViewModel
            {
                Email = account.Email,
                UserName = account.UserName,
                Name = user.Name,
                PhoneNumber = account.PhoneNumber,
                Location = user.Location,
                Role = (await _userManager.GetRolesAsync(account))[0].ToString()
            };

            if (model.Role.Equals("PeerCoach"))
            {
                model.UnassignedPatients = await GetUnassignedUsers();
                model.AssignedPatients = (new PeerCoach(_pulseContext, user.Id)).AssignedPatients;
            }
            else
            {
                model.AssignedPatients = new List<User>();
                model.UnassignedPatients = new List<User>();
            }

            if (model.Role.Equals("Patient"))
            {
                var assignedPeerCoach = (new Patient(user.Id, _pulseContext)).AssignedPeerCoach;
                model.AssignedPeerCoach = assignedPeerCoach.Name != null ? (new Patient(user.Id, _pulseContext)).AssignedPeerCoach.Name : "";
                model.AssignedPatients = new List<User>();
                model.UnassignedPatients = new List<User>();
            }


            return View("Views/UserManagement/Edit.cshtml", model);
        }

        private async Task<List<User>> GetUnassignedUsers()
        {
            var assignedPatients = _pulseContext.Assignment.Select(a => a.PatientId).ToList();
            var unassignedPatients = _pulseContext.User.Where(u => !assignedPatients.Contains(u.Id)).ToList();
            var returnValue = new List<User>();
            foreach (var patient in unassignedPatients)
            {
                var user = await _userManager.FindByIdAsync(patient.AccountId);
                if(await _userManager.IsInRoleAsync(user, "Patient"))
                {
                    returnValue.Add(patient);
                }
            }
            return returnValue;
        }

        [HttpPost()]
        [Route("Users/Edit")]
        public async Task<IActionResult> Edit(EditUserViewModel input)
        {
            var account = await _userManager.FindByNameAsync(input.UserName);
            var user = _pulseContext.User.Where(u => u.AccountId == account.Id).ToList()[0];

            if (ModelState.IsValid)
            {
                account.Email = account.Email;
                user.Name = input.Name;
                account.PhoneNumber = input.PhoneNumber;
                user.Location = input.Location;

                var result = await _userManager.UpdateAsync(account);

                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(account, (await _userManager.GetRolesAsync(account))[0].ToString());
                    await _userManager.AddToRoleAsync(account, input.Role);

                    await _pulseContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Index");
        }

        [Route("Users/{username}/AssignPatient/{patientId}")]
        public async Task<IActionResult> Assign(string username, string patientId)
        {
            var account = await _userManager.FindByNameAsync(username);
            var user = _pulseContext.User.Where(u => u.AccountId == account.Id).ToList()[0];

            _pulseContext.Assignment.Add(new Assignment
            {
                Id = Guid.NewGuid().ToString(),
                PeerCoachId = user.Id,
                PatientId = patientId,
                StartedOn = DateTime.Now
            });

            await _pulseContext.SaveChangesAsync();

            return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "EditUser", action = "Edit", Id = account.Id }));
        }

        [Route("Users/{username}/UnassignPatient/{patientId}")]
        public async Task<IActionResult> Unassign(string username, string patientId)
        {
            var account = await _userManager.FindByNameAsync(username);
            var user = _pulseContext.User.Where(u => u.AccountId == account.Id).ToList()[0];
            var assignment = _pulseContext.Assignment.Where(a => a.PatientId == patientId && a.PeerCoachId == user.Id).ToList()[0];
            _pulseContext.Remove(assignment);
            await _pulseContext.SaveChangesAsync();
            return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "EditUser", action = "Edit", Id = account.Id }));
        }
    }
}