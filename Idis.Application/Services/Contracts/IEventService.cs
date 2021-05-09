using Idis.Infrastructure;
using System.Data;

namespace Idis.Application
{
    public interface IEventService : IServiceBase<EventModel, Event>
    {
        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(EventModel model);
        bool DeleteByTitle(string title);
    }
}