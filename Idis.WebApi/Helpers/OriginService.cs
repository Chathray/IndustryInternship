using Idis.Application;

namespace Idis.WebApi
{
    public class OriginService : IOriginService
    {
        public UserModel User { get; set; }
        public int UserId
        {
            get { return User.UserId; }
            set { User.UserId = value; }
        }
        public string Email
        {
            get { return User.Email; }
            set { User.Email = value; }
        }
    }
}