using System.Data;

namespace Idis.Infrastructure
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUser(string email, string password);
        bool InsertUser(User user, string password);
        DataTable GetProfile(int id);
        bool UpdatePassword(int userId, string hash);
        bool UpdateBasic(User user);
        bool UserDelete(int userId);
        bool SetField(int userId, string field, dynamic value);
    }
}
