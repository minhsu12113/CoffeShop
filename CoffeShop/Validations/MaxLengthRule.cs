using CoffeShop.Dependency;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeShop.Validations
{
   public class MaxLengthRule : ValidationRule
   {
        private LengthChecker _lengthLimit;
        public LengthChecker LengthLimit
        {
            get { return _lengthLimit; }
            set { _lengthLimit = value; }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isValid = value.ToString().Length < LengthLimit.Maximum;
            return new ValidationResult(isValid, "Vượt quá số ký tự cho phép");
        }
    }
}
