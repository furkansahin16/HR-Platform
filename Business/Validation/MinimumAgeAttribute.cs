using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateOfBirth = (DateTime)value;
            var minStartDate = dateOfBirth.AddYears(_minimumAge);

            if (minStartDate > DateTime.Now)
            {
                return new ValidationResult($"Başlangıç tarihi için minimum yaş {_minimumAge} yıldır.");
            }

            return ValidationResult.Success;
        }
    }
}
