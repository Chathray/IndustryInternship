using Idis.Infrastructure;

namespace Idis.Application
{
    public interface IActivityService : IServiceBase<ActivityModel, Activity>
    {
        bool CleanAll(int userId);
    }
}