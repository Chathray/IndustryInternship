namespace Idis.Infrastructure.Test
{
    public class Builders
    {
        public static User GetUserDefault()
        {
            return new User
            {
                Email = "admin@x",
                FirstName = "Thanh",
                LastName = "Le",
                PasswordHash = "$2a$11$ZwUGQzP5M.gaE/FzHrbGDuNJrWhefvsoiTmyIDowKnhZuXRMBtux6"
            };
        }

    }
}