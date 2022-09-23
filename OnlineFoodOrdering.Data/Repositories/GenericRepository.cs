using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineFoodOrdering.Data.DbContexts;
using OnlineFoodOrdering.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Data.Repositories
{
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TSource> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<TSource>();
        }
        public Task<bool> AnyAsync(Expression<Func<TSource, bool>> expression)
            => _dbSet.AnyAsync(expression);

        public ValueTask<EntityEntry<TSource>> CreateAsync(TSource source)
            => _dbSet.AddAsync(source);

        public Task CreateRangeAsync(IEnumerable<TSource> sources)
            => _dbSet.AddRangeAsync(sources);

        public void DeleteRange(IEnumerable<TSource> sources)
            => _dbSet.RemoveRange(sources);

        public Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression)
            => _dbSet.FirstOrDefaultAsync(expression);

        public TSource Update(TSource source)
            => _dbSet.Update(source).Entity;

        public IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null)
            => expression is null ? _dbSet : _dbSet.Where(expression);
    }
}
