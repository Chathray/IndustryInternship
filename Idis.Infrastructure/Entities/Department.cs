using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Departments")]
    public class Department : EntityBase
    {
        [Key]
        public int DepartmentId { get; private set; }
        public string DepName { get; private set; }
        public string DepLocation { get; private set; }
        public string SharedTrainings { get; set; }

        private Department() { }
    }
}