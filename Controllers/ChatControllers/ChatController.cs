using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using Pulse.ViewModels.AssignmentViewModels;
using Pulse.ViewModels.ChatViewModels;
using Web.Controllers.ResourcesControllers;
using Web.ViewModels.ChatViewModels;

namespace Pulse.Controllers.ChatControllers
{
    public class ChatController : Controller
    {
        private readonly PulseContext _pulseContext;
        private readonly UserManager<Account> _userManager;
        private readonly ResourcesController _resourcesController;
        private ChatViewModel _model;
        private ChatIndexViewModel _coachIndex;
        private ChatAdminViewModel _adminIndex;
        private User _user;
        private Account _account;
        private bool _isAdmin;
        private bool _isPeerCoach;
        private bool _isPatient;

        public ChatController(PulseContext pulseContext, UserManager<Account> userManager)
        {
            _pulseContext = pulseContext;
            _userManager = userManager;
            _model = new ChatViewModel();
            _coachIndex = new ChatIndexViewModel();
            _adminIndex = new ChatAdminViewModel();
            _resourcesController = new ResourcesController();
        }

        [Route("Chat")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await SetUp();

            if (_isPatient)
            {
                SetUpPatient();
            }
            else if (_isPeerCoach)
            {
                var peerCoach = new PeerCoach(_pulseContext, _user.Id);
                _coachIndex.AssignedPatients = peerCoach.AssignedPatients;
                _coachIndex.PeerCoach = _user;
                return View("Views/Chat/CoachIndex.cshtml", _coachIndex);
            }
            else
            {
                SetUpAdmin();
                return View("Views/Chat/AdminIndex.cshtml", _adminIndex);
            }

            return View("Views/Chat/Chat.cshtml", _model);
        }

        [Route("Chat/{peerCoachId}/{patientId}")]
        [Authorize]
        public async Task<IActionResult> ViewChat(string peerCoachId, string patientId)
        {
            await SetUp(peerCoachId, patientId);
            return View("Views/Chat/Chat.cshtml", _model);
        }

        private void SetUpAdmin()
        {
            _adminIndex.Assignments = new List<AssignmentViewModel>();
            var assignments = _pulseContext.Assignment.ToList();
            foreach (var assignment in assignments)
            {
                _adminIndex.Assignments.Add(
                    new AssignmentViewModel
                    {
                        AssignedDate = assignment.StartedOn,
                        PeerCoach = _pulseContext.User.Where(u => u.Id == assignment.PeerCoachId).FirstOrDefault(),
                        Patient = _pulseContext.User.Where(u => u.Id == assignment.PatientId).FirstOrDefault()
                    }
                );
            }
        }

        private async Task SetUp(string peerCoachId, string patientId)
        {
            await SetUp();
            var peerCoach = new PeerCoach(_pulseContext, peerCoachId);
            var patient = new Patient(patientId, _pulseContext);
            _model.Patient = peerCoach.AssignedPatients.Where(a => a.Id == patientId).FirstOrDefault();
            _model.PeerCoach = patient.AssignedPeerCoach;
            _model.MessageHistory = patient.MessageHistory;
            _model.Sender = _user;
            _model.Receiver = _model.Patient;
        }

        private async Task SetUp()
        {
            _account = await _userManager.FindByNameAsync(User.Identity.Name);
            _user = _pulseContext.User.Where(u => u.AccountId == _account.Id).FirstOrDefault();
            _isAdmin = await _userManager.IsInRoleAsync(_account, "Admin");
            _isPeerCoach = await _userManager.IsInRoleAsync(_account, "PeerCoach");
            _isPatient = await _userManager.IsInRoleAsync(_account, "Patient");
            _model.Sender = _user;
            _model.Resources = _resourcesController.GetResources();
        }

        private void SetUpPatient()
        {
            var patient = new Patient(_user.Id, _pulseContext);
            _model.Patient = _user;
            _model.Receiver = patient.AssignedPeerCoach;
            _model.PeerCoach = patient.AssignedPeerCoach;
            _model.MessageHistory = patient.MessageHistory;
        }
    }
}