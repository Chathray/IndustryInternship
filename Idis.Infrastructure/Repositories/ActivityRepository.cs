namespace Idis.Infrastructure
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(DataContext context) : base(context)
        { }
    }
}
