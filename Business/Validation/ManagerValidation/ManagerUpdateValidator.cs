using Business.ViewModels.Common;
using FluentValidation;

namespace Business.Validation.ManagerValidation
{
    public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(x => x.PhoneNumber).Matches(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}").WithMessage("Hatalı telefon formatı");
        }
    }
}
