using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Questions")]
    public class Question : EntityBase
    {
        [Key]
        public int QuestionId { get; set; }
        public string Group { get; set; }
        public string InData { get; set; }
        public string OutData { get; set; }
    }
}