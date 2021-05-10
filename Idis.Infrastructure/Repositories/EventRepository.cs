using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace Idis.Infrastructure
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        { }

        public bool CheckOne(string title)
        {
            return _context.Events.SingleOrDefault(o => o.Title == title) is null;
        }

        public bool DeleteByTitle(string title)
        {
            var even = _context.Events.SingleOrDefault(_ => _.Title == title);
            if (even is null) return false;

            _context.Events.Remove(even);

            var effected = SaveChanges(nameof(DeleteByTitle));
            return effected > 0;
        }

        public DataTable GetJointEvents()
        {
            return _context.Database.GetDbConnection()
                .ExecReader($"CALL GetJointEvents()");
        }

        public string GetJson()
        {
            var obj = _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetEventsJson()");

            return obj as string;
        }

        public bool UpdateByTitle(Event model)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Image", model.Image);
            parameter.Add("Title", model.Title);
            parameter.Add("Type", model.Type);
            parameter.Add("ClassName", model.ClassName);
            parameter.Add("Start", model.Start);
            parameter.Add("End", model.End);
            parameter.Add("CreatedBy", model.CreatedBy, DbType.Int32);
            parameter.Add("GestsField", model.GestsField);
            parameter.Add("RepeatField", model.RepeatField);
            parameter.Add("EventLocationLabel", model.EventLocationLabel);
            parameter.Add("EventDescriptionLabel", model.EventDescriptionLabel);

            return _context.Database.GetDbConnection()
                .Execute($@"CALL UpdateEventByTitle(
                          @Image
                        , @Title
                        , @Type
                        , @ClassName
                        , @Start
                        , @End
                        , @CreatedBy
                        , @GestsField
                        , @RepeatField
                        , @EventLocationLabel
                        , @EventDescriptionLabel)", parameter) > 0;
        }
    }
}
