using Microsoft.AspNetCore.SignalR;

namespace Admin.Web.Models.ChatHub
{

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Save the message to the database using Dapper
            // Example: YourDapperRepository.SaveMessage(user, message);

            // Broadcast the message to all clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
