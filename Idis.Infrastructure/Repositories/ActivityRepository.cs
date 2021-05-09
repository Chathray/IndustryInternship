using Microsoft.EntityFrameworkCore;

namespace Idis.Infrastructure
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(DataContext context) : base(context)
        { }

        public bool CleanAll(int userId)
        {
            var checkout = _context.Database.ExecuteSqlRaw($@"
                DELETE FROM activities
                WHERE CreatedBy = {userId}
                ");

            return checkout > 0;
        }
    }
}
