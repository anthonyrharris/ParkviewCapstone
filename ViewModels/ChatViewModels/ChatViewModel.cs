using Pulse.EntityFramework.Models;
using Pulse.EntityFramework.Models.Resource;
using System.Collections.Generic;

namespace Pulse.ViewModels.ChatViewModels
{
    public class ChatViewModel
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public List<Message> MessageHistory { get; set; }
        public User PeerCoach { get; set; }
        public User Patient { get; set; }
        public List<Resource> Resources { get; set; }
    }
}
