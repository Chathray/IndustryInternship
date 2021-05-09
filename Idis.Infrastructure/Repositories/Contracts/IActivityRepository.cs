namespace Idis.Infrastructure
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {
        bool CleanAll(int userId);
    }
}
