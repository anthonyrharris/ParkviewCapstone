using System.Collections.Generic;
using Pulse.EntityFramework.Models;

namespace Web.ViewModels.ChatViewModels
{
    public class ChatIndexViewModel
    {
        public List<User> AssignedPatients { get; set; }
        public User PeerCoach { get; set; }
    }
}
