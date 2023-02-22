using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class TcknAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string tckn = (string)value;
            if (string.IsNullOrEmpty(tckn) || tckn.Length != 11)
            {
                return new ValidationResult("TCKN 11 haneli olmalıdır.");
            }

            if (!Regex.IsMatch(tckn, @"^[0-9]*$"))
            {
                return new ValidationResult("TCKN yalnızca rakam içerebilir.");
            }

            int total = 0;
            for (int i = 0; i < 10; i++)
            {
                total += int.Parse(tckn[i].ToString()) * (i + 1);
            }

            int mod = total % 11;
            int checkDigit = mod < 10 ? mod : mod - 10;
            if (checkDigit != int.Parse(tckn[10].ToString()))
            {
                return new ValidationResult("Geçersiz TCKN.");
            }

            return ValidationResult.Success;
        }
    }
}
