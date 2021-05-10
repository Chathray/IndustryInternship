using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}