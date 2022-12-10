using Intsoft.Exam.Application.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Intsoft.Exam.Application.Dtos.Inputs
{
    public class UserInput
    {
        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [EmailAddress]
        public string Email{ get; set; }

        [Required]
        public string Password{ get; set; }
    }
}
