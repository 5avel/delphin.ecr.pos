using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Deplphin.ECR.Pos.ViewsModels.Helpers
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double result = 0.0;
            bool canConvert = double.TryParse(value as string, out result);
            return new ValidationResult(canConvert, "Not a valid double");
        }
    }
}
