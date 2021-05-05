namespace Idis.Application
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IUserService _userService;
        private readonly IInternService _internService;
        private readonly IPointService _pointService;
        private readonly IDepartmentService _departmentService;
        private readonly IOrganizationService _organizationService;
        private readonly ITrainingService _trainingService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IQuestionService _questionService;
        private readonly IActivityService _activityService;

        public ServiceFactory(
            IUserService userService,
            IInternService internService,
            IPointService pointService,
            IDepartmentService departmentService,
            ITrainingService trainingService,
            IEventTypeService eventTypeService,
            IQuestionService questionService,
            IEventService eventService,
            IOrganizationService organizationService,
            IActivityService activityService)
        {
            _userService = userService;
            _internService = internService;
            _pointService = pointService;
            _departmentService = departmentService;
            _organizationService = organizationService;
            _trainingService = trainingService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
            _questionService = questionService;
            _activityService = activityService;
        }


        #region GET
        public IUserService User => _userService;

        public IInternService Intern => _internService;

        public IPointService Point => _pointService;

        public IDepartmentService Department => _departmentService;

        public IOrganizationService Organization => _organizationService;

        public ITrainingService Training => _trainingService;

        public IEventService Event => _eventService;

        public IEventTypeService EventType => _eventTypeService;

        public IQuestionService Question => _questionService;

        public IActivityService Activity => _activityService;
        #endregion GET

        public object GetAll(string field)
        {
            return field switch
            {
                "Training" => Training.GetAll(),
                "Organization" => Organization.GetAll(),
                "Department" => Department.GetAll(),
                "Activity" => Activity.GetAll(),
                _ => null,
            };
        }
    }
}
