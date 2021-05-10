namespace Idis.Application
{
    public class EventModel
    {
        public int EventId { get; set; }
        public int? CreatedBy { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Deadline { get; set; }
        public string RepeatField { get; set; }
        public string GestsField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }
        public string Image { get; set; }
    }
}