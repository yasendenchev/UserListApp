using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class EmailValidationAttribute : ValidationAttribute
{
    private static readonly Regex emailRegex = new Regex(
        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private const int MIN_LENGTH = 1;
    private const int MAX_LENGTH = 100;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var email = value as string;

        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult.Success;
        }

        if (email.Length < MIN_LENGTH || email.Length > MAX_LENGTH)
        {
            return new ValidationResult($"Email must be between {MIN_LENGTH} and {MAX_LENGTH} characters long.");
        }

        if (!emailRegex.IsMatch(email))
        {
            return new ValidationResult("Invalid email format.");
        }

        return ValidationResult.Success;
    }
}
