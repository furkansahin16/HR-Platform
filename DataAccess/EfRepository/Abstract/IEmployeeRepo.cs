using Core.Entites.Concrete;
using DataAccess.EfContext;
using DataAccess.EfRepository.Abstract.Common;

namespace DataAccess.EfRepository.Abstract
{
    public interface IEmployeeRepo : IEntityRepo<Employee, HRDbContext>
    {


    }
}
