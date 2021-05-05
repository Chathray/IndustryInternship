using System.Data;

namespace Idis.Infrastructure
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        DataTable GetJointEvents();
        string GetJson();
        bool CheckOne(string title);
        bool UpdateByTitle(Event aEvent);
    }
}
