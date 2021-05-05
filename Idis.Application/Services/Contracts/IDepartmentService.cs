using Idis.Infrastructure;

namespace Idis.Application
{
    public interface IDepartmentService : IServiceBase<DepartmentModel, Department>
    {
        bool InsertSharedTraining(int sharedId, int depId);
    }
}