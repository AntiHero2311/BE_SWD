using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BE_SWD.Models.ValidationAttributes
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Phone is optional
            }

            string phone = value.ToString()!;

            // Check if phone starts with 0 and has exactly 10 digits
            if (!phone.StartsWith("0") || phone.Length != 10 || !phone.All(char.IsDigit))
            {
                return new ValidationResult("Phone number must be exactly 10 digits and start with 0");
            }

            return ValidationResult.Success;
        }
    }
} 