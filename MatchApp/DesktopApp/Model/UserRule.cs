using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MatchApp.Desktop.Model
{
    public class UserRule : ValidationRule
    {     
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "Value cannot be empty.");          
            {
                if (value.ToString().Length > Max)
                    return new ValidationResult
                    (false, "Name cannot be more than " +Max.ToString());
                if (value.ToString().Length < Min)
                    return new ValidationResult
               (false, "Name cannot be less than " + Min.ToString());
            }

            return ValidationResult.ValidResult;
        }
    }


    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match match = regex.Match(value.ToString());
            if (match == null || match == Match.Empty)
            {
                return new ValidationResult(false, "Please enter valid email");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
