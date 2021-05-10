using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Idis.Infrastructure.Test
{
    public class UserTests
    {
        private readonly IUserRepository _mockUserRepo;

        private readonly UserRepository _memUserRepo;
        private readonly DataContext _memContext;

        public UserTests()
        {
            var existingUser = Builders.GetUserDefault();

            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "tmainternship")
                .Options;

            _memContext = new DataContext(dbOptions);
            _memUserRepo = new UserRepository(_memContext);

            var mockUserRepo = new Mock<IUserRepository>();

            mockUserRepo.Setup(o => o.GetOne(existingUser.UserId))
                .Returns(existingUser);

            mockUserRepo.Setup(o => o.UserDelete(existingUser.UserId))
                .Returns(true);

            // Complete the setup of our Mock Product Repository
            _mockUserRepo = mockUserRepo.Object;
        }

        [Fact]
        public void Get_Existing_User()
        {
            var existingUser = Builders.GetUserDefault();

            _memContext.Users.Add(existingUser);
            _memContext.SaveChanges();

            var testWithId = _memUserRepo.GetOne(existingUser.UserId);
            Assert.Equal(existingUser.UserId, testWithId.UserId);

            var testWithPassword = _memUserRepo.GetUser(existingUser.Email, "zazaza");
            Assert.Equal(existingUser.UserId, testWithPassword.UserId);
        }

        [Fact]
        public void Delete_Existing_User()
        {
            var existingUser = Builders.GetUserDefault();
            var result = _mockUserRepo.UserDelete(existingUser.UserId);
            Assert.True(result);
        }
    }
}