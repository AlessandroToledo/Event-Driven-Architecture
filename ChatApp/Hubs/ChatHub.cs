using ChatApp.Entities;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub(IChatService chatService) : Hub
    {
        private readonly IChatService _chatService = chatService;

        public async Task SendMessage(Message message)
        {
            _chatService.HandleMessage(message);
            await Clients.All.SendAsync("ReceiveMessage", message.User, message.Text);
        }
    }
}
