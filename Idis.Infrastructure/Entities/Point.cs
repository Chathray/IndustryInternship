using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Points")]
    public class Point : EntityBase
    {
        [Key]
        public int InternId { get; set; }
        public int MarkerId { get; set; }
        public float TechnicalSkill { get; set; }
        public float SoftSkill { get; set; }
        public float Attitude { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Score { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Passed { get; set; }


        [ForeignKey("InternId")]
        public Intern Intern { get; set; }

        [ForeignKey("MarkerId")]
        public User Marker { get; set; }
    }
}