using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class CompareDateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly int _years;

        public CompareDateAttribute(string comparisonProperty, int years)
        {
            _comparisonProperty = comparisonProperty;
            _years = years;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                throw new ArgumentException("Bu ada sahip bir tarih bulunamadı.");
            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if ((currentValue - comparisonValue).TotalDays < _years * 365)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
