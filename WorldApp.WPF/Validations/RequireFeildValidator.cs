using System.Globalization;
using System.Windows.Controls;

namespace WorldApp.WPF.Validations
{
    public class RequireFeildValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, Resources.Resources.FEILD_REQUIRED);
            if (value is string valueAsString && valueAsString.Length > 0)
                validationResult = ValidationResult.ValidResult;
            return validationResult;
        }
    }
}