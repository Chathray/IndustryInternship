using System.ComponentModel.DataAnnotations;

namespace Idis.WebApi
{
    public class CreateEventRequest
    {
        public int CreatedBy { get; set; }

        [Required]
        public string Title { get; set; }

        public string Type { get; set; }

        [Required]
        public string Deadline { get; set; }

        public string RepeatField { get; set; }

        public string GestsField { get; set; }

        public string EventLocationLabel { get; set; }

        public string EventDescriptionLabel { get; set; }

        public string Image { get; set; }
    }
}