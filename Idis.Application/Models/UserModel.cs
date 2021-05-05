namespace Idis.Application
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; } = null;
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }

        public string Status { get; set; } = "success";
        public string Role { get; set; } = "staff";
        public string HeaderPhoto { get; set; } = "/img/_header.jpg";
        public string Avatar { get; set; } = "/img/_user.jpg";
        public bool AvatarVisibility { get; set; } = true;
        public int? DepartmentId { get; set; }
        public string CreatedDate { get; set; }
    }
}