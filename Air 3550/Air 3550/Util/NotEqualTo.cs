using System.ComponentModel.DataAnnotations;

namespace Air_3550.Util
{
    class NotEqualTo : ValidationAttribute
    {
        private string _otherProperty;

        public NotEqualTo(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(_otherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
