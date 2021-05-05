using Idis.Infrastructure;
using System.Data;

namespace Idis.Application
{
    public interface IUserService : IServiceBase<UserModel, User>
    {
        UserModel Authenticate(string loginEmail, string loginPassword);
        int CountByIndex(int index);
        bool InsertUser(UserModel model);
        DataTable GetProfile(int id);
        bool UpdatePassword(int userId, string newPassword);
        bool UpdateBasic(UserModel user);
        bool UserDelete(int userId);
        bool SetStatus(int userId, string status);
    }
}