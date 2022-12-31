using System.Linq.Expressions;
using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
{
    public sealed class DataRepository<T> : IRepository<T>
      where T : class, IDataModel
    {
        private readonly DbContext _dbContext;

        public DataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>()
                .SingleOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);
        }

        public Task<IEnumerable<T>> GetAllAsync<TOrderByValue>(Expression<Func<T, bool>> filter, Expression<Func<T, TOrderByValue>> orderBy)
        {
            return Task.FromResult<IEnumerable<T>>(_dbContext.Set<T>().Where(filter).OrderBy(orderBy));
        }

        public async Task<T> InsertAsync(T entity)
        {
            var res = _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> func)
        {
            return await _dbContext.Set<T>().Where(func).ToListAsync()
                .ConfigureAwait(false);
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return _dbContext.SaveChangesAsync();
        }

        public long Max()
        {
            return _dbContext.Set<T>().Max(p => p.Id);
        }

        public long Count(Expression<Func<T, bool>> func)
        {
            return _dbContext.Set<T>().Count(func);
        }

        public Task<IEnumerable<T>> GetAllAsync<TOrderByValue>(Expression<Func<T, TOrderByValue>> orderBy)
        {
            return Task.FromResult<IEnumerable<T>>(_dbContext.Set<T>().OrderBy(orderBy));
        }

    }
}
