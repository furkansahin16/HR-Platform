using Core.Entites.Concrete;
using DataAccess.EfContext;
using DataAccess.EfRepository.Abstract;
using DataAccess.EfRepository.Concrete.Common;

namespace DataAccess.EfRepository.Concrete
{
    public class PermissionRepo : EFRepo<Permission, HRDbContext>, IPermissionRepo
    {
        public PermissionRepo(HRDbContext db) : base(db)
        {
        }
    }
}
