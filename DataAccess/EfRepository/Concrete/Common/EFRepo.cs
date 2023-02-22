using DataAccess.EfRepository.Abstract.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.EfRepository.Concrete.Common
{
    public class EFRepo<TEntity, TContext> : IEntityRepo<TEntity, TContext>
        where TEntity : class, new()
        where TContext : DbContext
    {
        TContext _db;
        public EFRepo(TContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return _db.Set<TEntity>().MyIncludes(includes).Where(filter);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter is null ? _db.Set<TEntity>() : _db.Set<TEntity>().Where(filter);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return (await _db.Set<TEntity>().MyIncludes().Where(filter).FirstAsync());
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return (await _db.Set<TEntity>().Where(filter).FirstAsync());
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
