using Core.Entites.Concrete;
using DataAccess.EfContext;
using DataAccess.EfRepository.Abstract;
using DataAccess.EfRepository.Concrete.Common;

namespace DataAccess.EfRepository.Concrete
{
    public class EmployeeRepo : EFRepo<Employee, HRDbContext>, IEmployeeRepo
    {
        public EmployeeRepo(HRDbContext db) : base(db)
        {
        }
    }
}
