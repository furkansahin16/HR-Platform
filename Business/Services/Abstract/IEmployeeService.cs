using Business.SystemResult;
using Business.ViewModels.Common;
using Core.Entites.Concrete;

namespace Business.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<ResultService<EmployeeUpdate>> UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);

        Task UpdateAnnualPermission(Employee employee);
    }
}
