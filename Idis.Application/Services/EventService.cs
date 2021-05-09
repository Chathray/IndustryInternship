using Idis.Infrastructure;
using System;
using System.Data;

namespace Idis.Application
{
    public class EventService : ServiceBase<EventModel, Event>, IEventService
    {
        private readonly IEventRepository _eventRepo;
        public EventService(IEventRepository eventRepo) : base(eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public bool DeleteByTitle(string title)
        {
            return _eventRepo.DeleteByTitle(title);
        }

        public DataTable GetEventsIntern()
        {
            return _eventRepo.GetJointEvents();
        }

        public string GetJson()
        {
            return _eventRepo.GetJson();
        }

        public bool InsertEvent(EventModel model)
        {
            Event obj = ObjectMapper.Mapper.Map<Event>(model);

            var dateArray = model.Deadline.Split(" - ");
            // Ngoại lệ ngày đơn
            try
            {
                obj.Start = DateTime.ParseExact(dateArray[0], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                obj.End = DateTime.ParseExact(dateArray[1], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            }
            catch { obj.End = obj.Start; }

            switch (model.Type)
            {
                case "fullcalendar-custom-event-hs-team":
                    obj.Type = "Personal";
                    break;
                case "fullcalendar-custom-event-holidays":
                    obj.Type = "Holidays";
                    break;
                case "fullcalendar-custom-event-tasks":
                    obj.Type = "Tasks";
                    break;
                case "fullcalendar-custom-event-reminders":
                    obj.Type = "Reminders";
                    break;
            }
            obj.ClassName = model.Type;

            if (_eventRepo.CheckOne(obj.Title))

                return _eventRepo.Create(obj);
            else
                return _eventRepo.UpdateByTitle(obj);

        }
    }
}
