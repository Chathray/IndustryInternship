using System.ComponentModel.DataAnnotations;

namespace Idis.Website
{
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        public string LoginEmail { get; set; }

        [Required]
        [MinLength(6)]
        public string LoginPassword { get; set; }

        public bool Remember { get; set; }

        /*-----------------------------------*/

        public string ResetEmail { get; set; }

        /*-----------------------------------*/

        public string RegiterFirstName { get; set; }

        public string RegiterLastName { get; set; }

        public string RegiterEmail { get; set; }

        public string RegiterPassword { get; set; }
    }
}
