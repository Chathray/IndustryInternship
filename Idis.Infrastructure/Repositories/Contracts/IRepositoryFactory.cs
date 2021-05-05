namespace Idis.Infrastructure
{
    public interface IRepositoryFactory
    {
        IUserRepository User { get; }
        IInternRepository Intern { get; }
        IPointRepository Point { get; }
        IDepartmentRepository Department { get; }
        IOrganizationRepository Organization { get; }
        ITrainingRepository Training { get; }
        IEventRepository Event { get; }
        IEventTypeRepository EventType { get; }
        IQuestionRepository Question { get; }
        void Save();
    }
}
