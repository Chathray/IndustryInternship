namespace Idis.Infrastructure
{
    public class TrainingRepository : RepositoryBase<Training>, ITrainingRepository
    {
        public TrainingRepository(DataContext context) : base(context)
        { }
    }
}