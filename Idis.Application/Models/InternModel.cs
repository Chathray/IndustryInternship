namespace Idis.Application
{
    public class InternModel
    {
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int TrainingId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public int MentorId { get; set; }
        public int InternId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}