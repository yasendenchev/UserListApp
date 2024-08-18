using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NameValidationAttribute : ValidationAttribute
{
    private static readonly Regex NameRegex = new Regex(
        @"^(?![-\s])(?:[\p{L}àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.']+(?:(?<![- ])-(?![ -])[\p{L}àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.']+)*(?:(?<![- ]) (?![ -])[\p{L}àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.']+(?:(?<![- ])-(?![ -])[\p{L}àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.']+)*)*)(?<![-\s])$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private const int MIN_LENGTH = 1;
    private const int MAX_LENGTH = 100;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var name = value as string;

        if (string.IsNullOrEmpty(name))
        {
            return ValidationResult.Success;
        }

        if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
        {
            return new ValidationResult($"Name must be between {MIN_LENGTH} and {MAX_LENGTH} characters long.");
        }

        if (!NameRegex.IsMatch(name))
        {
            return new ValidationResult("Invalid name format.");
        }

        return ValidationResult.Success;
    }
}
