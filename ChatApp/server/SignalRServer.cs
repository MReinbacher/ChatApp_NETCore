
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Data;
using System;

namespace ChatApp.Server
{
    public class SignalRServer : Hub
    {
        private readonly ChatContext _db;
        public SignalRServer(ChatContext db)
        {
            _db = db;
        }
        public async Task SendMessage(string user, string message)
        {
            ChatMessage msg = new ChatMessage
            {
                Message = message,
                User = user,
                Timestamp = DateTime.Now
            };
            await Clients.All.SendAsync("BroadcastMessage", msg);
            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();
        }
    }
}