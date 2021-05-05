using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace Idis.Infrastructure
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        { }

        public User GetUser(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            // check if not exists
            if (user is null || user.IsDeleted)
                return null;

            // check if password is correct
            if (!BC.Verify(password, user.PasswordHash))
                return null;

            // authentication successful
            return user;
        }

        public DataTable GetProfile(int id)
        {
            return _context.Database.GetDbConnection()
                .ExecReader($"CALL GetProfileView({id})");
        }

        public bool InsertUser(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (_context.Users.Any(x => x.Email == user.Email))
                return false;

            user.PasswordHash = BC.HashPassword(password);
            return Create(user);
        }

        public bool SetField(int userId, string field, dynamic value)
        {
            var user = GetOne(userId);
            switch (field)
            {
                case nameof(user.Status):
                    user.Status = value;
                    break;
                case nameof(user.Role):
                    user.Role = value;
                    break;
            }
            return SaveChanges($"Set{field}") > 0;
        }

        public bool UpdateBasic(User user)
        {
            return _context.Database.GetDbConnection()
                .ExecNonQuery($@"
                    UPDATE users
                    SET
                        FirstName = '{user.FirstName}'
                      , LastName = '{user.LastName}'
                      , Email = '{user.Email}'
                      , Phone = '{user.Phone}'
                      , DepartmentId = {user.DepartmentId}
                      , Role = '{user.Role}'
                      , Address1 = '{user.Address1}'
                      , Address2 = '{user.Address2}'
                      , ZipCode = '{user.ZipCode}'
                    WHERE
                        UserId = {user.UserId}");
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            var hash = BC.HashPassword(newPassword);
            return _context.Database.GetDbConnection()
                .ExecNonQuery($@"
                    UPDATE users
                    SET PasswordHash = '{hash}'
                    WHERE UserId = {userId}");
        }

        public bool UserDelete(int userId)
        {
            return _context.Database.GetDbConnection()
                .ExecNonQuery($@"
                    UPDATE users
                    SET IsDeleted = 1
                    WHERE UserId = {userId}");
        }
    }
}