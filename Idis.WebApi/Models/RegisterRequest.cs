using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
