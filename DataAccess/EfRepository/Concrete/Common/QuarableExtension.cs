using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.EfRepository.Concrete.Common
{
    public static class QuarableExtension
    {
        public static IQueryable<T> MyIncludes<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (a, b) => a.Include(b));
            }
            return query;
        }
    }
}
