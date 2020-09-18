using Pulse.EntityFramework.Models;
using System;

namespace Pulse.ViewModels.AssignmentViewModels
{
    public class AssignmentViewModel
    {
        public User PeerCoach { get; set; }
        public User Patient { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
