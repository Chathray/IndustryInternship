using Idis.Application;

namespace Idis.WebApi
{
    public interface IOriginService
    {
        UserModel User { get; set; }
        int UserId { get; set; }
        string Email { get; set; }
    }
}