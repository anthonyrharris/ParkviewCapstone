using Ganss.XSS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Pulse.EntityFramework.DataContext;
using Pulse.EntityFramework.Models;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<Account> _userManager;
        private readonly PulseContext _pulseContext;

        public ChatHub(UserManager<Account> userManager, PulseContext pulseContext)
        {
            _userManager = userManager;
            _pulseContext = pulseContext;
        }

        public async Task SendMessage(string receiverId, string recieverName, string senderId, string senderName, string message)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(message);

            Message msg = new Message
            {
                Id = Guid.NewGuid().ToString(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = 1,
                Content = sanitized,
                DateSent = DateTime.Now
            };

            _pulseContext.Message.Add(msg);

            

            await Clients.All.SendAsync("ReceiveMessage", senderName, sanitized);
            var result = _pulseContext.SaveChanges();
            Console.WriteLine("Hello!");
        }
    }

}