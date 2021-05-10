using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class StatusRequest
    {
        [Required]
        [DisplayName("User Status")]
        [Description("Express the current state of a user")]
        [RegularExpression("success|danger|warning", ErrorMessage = "Only following names are allowed")]
        public string Status { get; set; }
    }
}