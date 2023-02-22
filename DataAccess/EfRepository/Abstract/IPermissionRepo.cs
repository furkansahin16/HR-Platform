using Core.Entites.Concrete;
using DataAccess.EfContext;
using DataAccess.EfRepository.Abstract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EfRepository.Abstract
{
    public interface IPermissionRepo : IEntityRepo<Permission,HRDbContext>
    {
    }
}
