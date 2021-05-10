using Idis.Application;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Idis.Website
{
    public class EchoHub : Hub
    {
        private readonly IServiceFactory _serviceFactory;

        public EchoHub(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public string UserId => Context.UserIdentifier;

        public async Task ToastMaster(string message, string owner)
        {
            await Clients.All.SendAsync("ClientMasterMessage", message, owner);

            var log = new ActivityModel
            {
                ActivityName = "Create a Toast Master",
                CreatedBy = int.Parse(owner),
                ActivityDescription = message,
            };

            _serviceFactory.Activity.Create(log);
        }

        public async Task SendUserStatus(string message)
        {
            await Clients.User(UserId).SendAsync("ClientStatus", message);
        }

        public bool AvatarVisibility(string value, string owner)
        {
            var userId = int.Parse(owner);
            var visible = bool.Parse(value);

            var result = _serviceFactory.User.SetAvatarVisibility(userId, visible);

            if (result)
            {
                var log = new ActivityModel
                {
                    ActivityName = "Update User Info",
                    CreatedBy = userId,
                    ActivityDescription = "Set Avatar Visibility to " + (visible ? "Anyone" : "Only you"),
                };

                _serviceFactory.Activity.Create(log);
            }

            return result;
        }

        public bool CleanActivities(string uuid)
        {
            int userId = int.Parse(uuid);
            bool result = _serviceFactory.Activity.CleanAll(userId);
            return result;
        }
    }
}