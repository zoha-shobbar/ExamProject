using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Intsoft.Exam.API.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Phone number should not be empty");
            }

            string pattern = @"^09[0|1|2|3][0-9]{8}$";
            Regex reg = new Regex(pattern);
            var isValid = reg.IsMatch((string)value);

            if (isValid)
                return ValidationResult.Success;
            else
                return new ValidationResult("Phone number is not valid");
        }
    }
}
