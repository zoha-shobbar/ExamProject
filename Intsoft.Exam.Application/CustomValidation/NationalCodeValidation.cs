using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Intsoft.Exam.Application.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NationalCodeValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("NationalCode should not be empty");
            }

            string pattern = @"^[0-9]{10}$";
            Regex reg = new Regex(pattern);
            var isValid = reg.IsMatch((string)value);

            if (isValid)
                return ValidationResult.Success;
            else
                return new ValidationResult("NationalCode is not valid");
        }
    }
}
