using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class CreateInternRequest
    {
        public int? InternId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int? TrainingId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? MentorId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}