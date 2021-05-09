namespace Idis.Infrastructure
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        bool InsertSharedTraining(int sharedId, int depId);
        bool RefreshSharedTrainings(int traId);
    }
}
