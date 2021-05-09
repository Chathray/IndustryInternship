using Idis.Infrastructure;
using System.Data;

namespace Idis.Application
{
    public class UserService : ServiceBase<UserModel, User>, IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo) : base(userRepo)
        {
            _userRepo = userRepo;
        }

        public UserModel Authenticate(string loginEmail, string loginPassword)
        {
            var user = _userRepo.GetUser(loginEmail, loginPassword);

            if (user is null) return null;

            // check if dead, make it to alive
            if (user.IsDeleted)
            {
                var success =_userRepo.UnDelete(user.UserId);
                if (!success) return null;
            }
            var model = ObjectMapper.Mapper.Map<UserModel>(user);
            return model;
        }

        public DataTable GetProfile(int id)
        {
            return _userRepo.GetProfile(id);
        }

        public bool InsertUser(UserModel model)
        {
            var user = ObjectMapper.Mapper.Map<User>(model);
            return _userRepo.InsertUser(user, model.Password);
        }

        public bool SetStatus(int userId, string status)
        {
            return _userRepo.SetField(userId, nameof(User.Status), status);
        }

        public bool SetAvatarVisibility(int userId, bool value)
        {
            return _userRepo.SetField(userId, nameof(User.AvatarVisibility), value);
        }

        public bool SetRole(int userId, string status)
        {
            return _userRepo.SetField(userId, nameof(User.Role), status);
        }

        public bool UpdateBasic(UserModel model)
        {
            var user = ObjectMapper.Mapper.Map<User>(model);
            return _userRepo.UpdateBasic(user);
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            return _userRepo.UpdatePassword(userId, newPassword);
        }

        public bool UserDelete(int userId)
        {
            return _userRepo.UserDelete(userId);
        }

        public int GetProfileValue(int userId)
        {
            return _userRepo.GetProfileValue(userId);
        }
    }
}
