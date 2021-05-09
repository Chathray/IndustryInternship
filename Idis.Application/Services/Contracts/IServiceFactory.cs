namespace Idis.Application
{
    public interface IServiceFactory
    {
        IUserService User { get; }
        IInternService Intern { get; }
        IPointService Point { get; }
        IDepartmentService Department { get; }
        IOrganizationService Organization { get; }
        ITrainingService Training { get; }
        IEventService Event { get; }
        IEventTypeService EventType { get; }
        IQuestionService Question { get; }
        IActivityService Activity { get; }

        object GetAll(string field);
    }
}
