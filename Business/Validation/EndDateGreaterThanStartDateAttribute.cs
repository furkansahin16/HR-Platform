using Business.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (EmployeeRegister)validationContext.ObjectInstance;
            if (model.DismissalDate != null && model.DismissalDate < model.StartDateOfWork)
            {
                return new ValidationResult("İşten Ayrılma Tarihi, İşe Başlama Tarihinden büyük olmalıdır");
            }
            return ValidationResult.Success;
        }
    }
}
