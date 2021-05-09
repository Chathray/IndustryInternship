using Idis.Infrastructure;

namespace Idis.Application
{
    public class ActivityService : ServiceBase<ActivityModel, Activity>, IActivityService
    {
        private readonly IActivityRepository _activityRepo;
        public ActivityService(IActivityRepository activityRepo) : base(activityRepo)
        {
            _activityRepo = activityRepo;
        }

        public bool CleanAll(int userId)
        {
            return _activityRepo.CleanAll(userId);
        }
    }
}
