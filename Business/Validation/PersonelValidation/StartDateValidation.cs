using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.PersonelValidation
{
    public class StartDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime startTime = (DateTime)value;
 
            if (startTime < DateTime.Now || startTime > DateTime.Now.AddYears(1))
            {
                return new ValidationResult("İzin başlangıç tarihi bugün veya bugünden önceki bir tarih olamaz ve en fazla 1 yıl içinde olmalıdır.");
            }
            
            return ValidationResult.Success;
        }

    }
}