using Idis.Infrastructure;

namespace Idis.Application
{
    public class EventTypeService : ServiceBase<EventTypeModel, EventType>, IEventTypeService
    {
        private readonly IEventTypeRepository _evenTypeRepo;
        public EventTypeService(IEventTypeRepository evenTypeRepo) : base(evenTypeRepo)
        {
            _evenTypeRepo = evenTypeRepo;
        }
    }
}
