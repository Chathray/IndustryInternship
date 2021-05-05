using Idis.Infrastructure;

namespace Idis.Application
{
    public class TrainingService : ServiceBase<TrainingModel, Training>, ITrainingService
    {
        private readonly ITrainingRepository _trainingRepo;
        public TrainingService(ITrainingRepository trainingRepo) : base(trainingRepo)
        {
            _trainingRepo = trainingRepo;
        }
    }
}
