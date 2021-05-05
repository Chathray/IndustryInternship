using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Events")]
    public class Event : EntityBase
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string ClassName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string GestsField { get; set; }
        public string RepeatField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }
        public string Image { get; set; }


        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        [ForeignKey("UpdatedBy")]
        public User Editor { get; set; }
    }
}