using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace OnlineFoodOrdering.Data.Interfaces
{
    public interface IGenericRepository<TSource> where TSource : class
    {
        ValueTask<EntityEntry<TSource>> CreateAsync(TSource source);
        Task CreateRangeAsync(IEnumerable<TSource> sources);

        TSource Update(TSource source);
        Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TSource, bool>> expression);
        IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null);
        void DeleteRange(IEnumerable<TSource> sources);
    }
}
