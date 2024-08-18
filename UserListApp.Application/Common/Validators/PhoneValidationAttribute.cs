using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PhoneValidationAttribute : ValidationAttribute
{
    private static readonly Regex phoneRegex = new Regex(
        @"^\+?[1-9]\d{1,14}([-.\s]?\d{1,13}){1,5}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private const int MIN_LENGTH = 8;
    private const int MAX_LENGTH = 30;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phone = value as string;

        if (string.IsNullOrEmpty(phone))
        {
            return ValidationResult.Success;
        }

        if (phone.Length < MIN_LENGTH || phone.Length > MAX_LENGTH)
        {
            return new ValidationResult($"Phone must be between {MIN_LENGTH} and {MAX_LENGTH} characters long.");
        }

        if (!phoneRegex.IsMatch(phone))
        {
            return new ValidationResult("Invalid phone format.");
        }

        return ValidationResult.Success;
    }
}
