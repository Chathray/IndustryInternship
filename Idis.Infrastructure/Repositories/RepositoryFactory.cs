namespace Idis.Infrastructure
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataContext _repositoryContext;
        private readonly IUserRepository _userRepository;
        private readonly IInternRepository _internRepository;
        private readonly IPointRepository _pointRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IQuestionRepository _questionRepository;

        public RepositoryFactory(
            DataContext repositoryContext,
            IUserRepository userRepository,
            IInternRepository internRepository,
            IPointRepository pointRepository,
            IDepartmentRepository departmentRepository,
            ITrainingRepository trainingRepository,
            IEventTypeRepository eventTypeRepository,
            IQuestionRepository questionRepository,
            IEventRepository eventRepository,
            IOrganizationRepository organizationRepository
            )
        {
            _repositoryContext = repositoryContext;
            _userRepository = userRepository;
            _internRepository = internRepository;
            _pointRepository = pointRepository;
            _departmentRepository = departmentRepository;
            _organizationRepository = organizationRepository;
            _trainingRepository = trainingRepository;
            _eventRepository = eventRepository;
            _eventTypeRepository = eventTypeRepository;
            _questionRepository = questionRepository;
        }


        #region GET
        public IUserRepository User => _userRepository;

        public IInternRepository Intern => _internRepository;

        public IPointRepository Point => _pointRepository;

        public IDepartmentRepository Department => _departmentRepository;

        public IOrganizationRepository Organization => _organizationRepository;

        public ITrainingRepository Training => _trainingRepository;

        public IEventRepository Event => _eventRepository;

        public IEventTypeRepository EventType => _eventTypeRepository;

        public IQuestionRepository Question => _questionRepository;
        #endregion GET


        public void Save() => _repositoryContext.SaveChanges();
    }
}
