using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MyHomeServer.Shared.Models;
using Server.Data;

namespace MyHomeServer.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, username);
            await AddMessageToChat(new MessageDTO() { 
                SenderUser = username,
                Content = "під'єднався!",
                SendDate = DateTime.Now
            });
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(new MessageDTO()
            {
                SenderUser = username,
                Content = "вийшов.",
                SendDate = DateTime.Now
            });
        }

        public async Task AddMessageToChat(MessageDTO message)
        {
            await Clients.All.SendAsync("GetMessage", message);
        }
    }
}
