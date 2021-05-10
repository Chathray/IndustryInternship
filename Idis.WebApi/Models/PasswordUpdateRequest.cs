using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class PasswordUpdateRequest
    {
        [Required]
        [MinLength(6)]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}