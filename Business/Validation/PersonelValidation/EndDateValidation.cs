using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.PersonelValidation
{
    public class EndDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime endTime = (DateTime)value;
            DateTime startTime = (DateTime)validationContext.ObjectType.GetProperty("StartTime").GetValue(validationContext.ObjectInstance);
            if (endTime < startTime )
            {
                return new ValidationResult("İzin bitiş tarihi başlangıç tarihinden önce olamaz.");
            }

            return ValidationResult.Success;
        }
    }
}