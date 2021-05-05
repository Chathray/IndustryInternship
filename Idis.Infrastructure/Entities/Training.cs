using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Trainings")]
    public class Training : EntityBase
    {
        [Key]
        public int TrainingId { get; set; }
        public string TraName { get; set; }
        public string TraData { get; set; }
        public int? CreatedBy { get; set; }


        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
    }
}