using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Idis.Website
{
    public class EchoHub : Hub
    {
        public string UserId => Context.UserIdentifier;

        public async Task ToastMaster(string message, string owner)
        {

            await Clients.All.SendAsync("ClientMasterMessage", message, owner);
        }

        public async Task SendUserStatus(string message)
        {
            await Clients.User(UserId).SendAsync("ClientStatus", message);
        }
    }
}