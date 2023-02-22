using Business.Helper.ImageConverter;
using Business.Services.Abstract;
using Business.SystemResult;
using Business.SystemResult.Enums;
using Business.ViewModels.Common;
using DataAccess.EfRepository.Abstract;
using Business.Helper.ImageConverter;
using Core.Entites.Concrete;
using Business.Helper.PermissionHelper;

namespace Business.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _repo;

        public EmployeeService(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Çalışan şirkette yeni bir yılı tamamladığında çalıştığı yıl sayısına göre yıllık izin hakkı çalışana eklenir.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task UpdateAnnualPermission(Employee employee)
        {
            var result = AnnualPermissionHelper.UpdateAnnualPermissionDay(employee.StartDateOfWork, employee.WorkTimeByYear, employee.AnnualPermission);

            employee.WorkTimeByYear = result.Item1;
            employee.AnnualPermission = result.Item2;

            try
            {
                await _repo.UpdateAsync(employee);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Verilen çalışan modeline göre çalışan bilgilerini günceller.
        /// </summary>
        /// <param name="employeeUpdate"></param>
        /// <returns></returns>
        public async Task<ResultService<EmployeeUpdate>> UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
        {
            ResultService<EmployeeUpdate> result = new();
            result.ResultData = employeeUpdate;
            try
            {
                var employee = await _repo.GetAsync(x => x.Id == employeeUpdate.Id);

                bool isPhotoChanged = employeeUpdate.NewPhoto != null;

                if (isPhotoChanged)
                {
                    if (employee.Photo != "default-user-image.png") employee.Photo.DeleteImageFromUrl();
                    employee.Photo = employeeUpdate.NewPhoto.ToStringUrl();
                }

                employee.Address = employeeUpdate.Address;
                employee.PhoneNumber = employeeUpdate.PhoneNumber;

                await _repo.UpdateAsync(employee);

                result.AddSuccess("Güncelleme işlemi başarılı.");
            }
            catch (Exception)
            {
                result.AddError("Güncelleme işlemi sırasında bir hata oluştu.");
            }
            return result;
        }
    }
}
