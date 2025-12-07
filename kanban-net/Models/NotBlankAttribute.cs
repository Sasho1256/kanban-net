using System.ComponentModel.DataAnnotations;

namespace kanban_net.Models
{
    public class NotBlankAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str)))
            {
                return new ValidationResult($"{validationContext.DisplayName} cannot be null, empty or whitespace.");
            }

            return ValidationResult.Success;
        }
    }
}