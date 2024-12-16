using System.ComponentModel.DataAnnotations;

namespace Application.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly float _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must not be empty.");
            }

            var payload = float.TryParse(value.ToString(), out var floatValue);

            if (!payload)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a number.");
            }

            if (floatValue < _minValue)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be greater than or equal to {_minValue}.", [validationContext.MemberName]);
            }

            return ValidationResult.Success;
        }
    }
}
