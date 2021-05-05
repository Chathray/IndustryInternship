namespace Idis.Application
{
    public class ActivityModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string OtherDetails { get; set; }
        public int? CreatedBy { get; set; }
    }
}