using System.ComponentModel.DataAnnotations;

namespace ExamProject.Application.Dtos.Inputs
{
    public class BookInput
    {
        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Summury { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public int PublishYear { get; set; }
    }
}
