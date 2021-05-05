using Idis.Application;
using System.Collections.Generic;

namespace Idis.WebApi
{
    public class EventModel
    {
        public EventModel() { }
        public EventModel(IList<EventTypeModel> eventtypes, string whitelist)
        {
            EvenTypes = eventtypes;
            Whitelist = whitelist;
        }

        public IList<EventTypeModel> EvenTypes { get; set; }
        public string Whitelist { get; set; }

        public string Creator { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Deadline { get; set; }
        public string RepeatField { get; set; }
        public string GestsField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }
    }
}