using System.Linq.Expressions;
using LibraryManagement.Data.Models;

namespace LibraryManagement.Data.Repositories
{
    /// <summary>
    /// Interface for DB interactions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IDataModel
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(long id);

        /// <summary>
        /// Get all async
        /// </summary>
        /// <typeparam name="TOrderByValue"></typeparam>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync<TOrderByValue>(Expression<Func<T, TOrderByValue>> orderBy);

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get by filter
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> func);
        Task<IEnumerable<T>> GetAllAsync<TOrderByValue>(Expression<Func<T, bool>> filter, Expression<Func<T, TOrderByValue>> orderBy);
        Task<T> InsertAsync(T entity);
        Task DeleteAsync(T entity);
        long Max();
        long Count(Expression<Func<T, bool>> func);
    }
}