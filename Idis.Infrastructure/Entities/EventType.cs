using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("EventTypes")]
    public class EventType : EntityBase
    {
        [Key]
        public string Type { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
    }
}