using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.EfRepository.Abstract.Common
{
    public interface IEntityRepo<TEntity, TContext>
        where TEntity : class, new()
        where TContext : DbContext
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
    }
}
