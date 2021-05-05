using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Activities")]
    public class Activity : EntityBase
    {
        [Key]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string OtherDetails { get; set; }
        public int? CreatedBy { get; set; }


        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
    }
}