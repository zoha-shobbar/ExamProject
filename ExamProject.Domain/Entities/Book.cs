using ExamProject.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        public string ISBN{ get; set; }

        [Required]
        public string Title { get; set; }
        public string? Summury{ get; set; }
        
        [Required]
        public string Publisher { get; set; }
        
        [Required]
        public int PublishYear { get; set; }

        public bool IsAvailable { get; set; }
    }
}
