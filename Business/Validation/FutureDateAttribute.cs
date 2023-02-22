using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var endDate = (DateTime)value;
        //    var today = DateTime.Today;

        //    if (endDate > today.AddMonths(2))
        //    {
        //        return new ValidationResult(ErrorMessage);
        //    }

        //    return ValidationResult.Success;


        //}
        public override bool IsValid(object value)
        {
            DateTime? endDate = value as DateTime?;
            if (endDate.HasValue)
            {
                if (endDate.Value > DateTime.Now.AddMonths(2))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
