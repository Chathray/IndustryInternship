using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idis.Infrastructure
{
    [Table("Users")]
    public class User : EntityBase
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public string HeaderPhoto { get; set; }
        public string Avatar { get; set; }
        public bool AvatarVisibility { get; set; }
        public bool IsDeleted { get; set; }
        public string ZipCode { get; set; }
        public int? DepartmentId { get; set; }


        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}