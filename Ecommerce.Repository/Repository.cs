using Ecommerce.Data;
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class RepositoryBase<T> : IRepository<T>  where T : BaseEntity  
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected IDbContextTransaction _dbTransaction; 
        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();

        }
        public T Create(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{T}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        protected TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                  Expression<Func<T, bool>> predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }


        public async Task<ICollection<T>> GetAsync(Expression<Func<T,bool>> expression)
        {
             return await _dbSet.Where(expression).ToArrayAsync();
        }

        public async Task<ICollection<Tselect>> GetAsync<Tselect>(Expression<Func<T, bool>> expression, Expression<Func<T, Tselect>> select)
        {
            return await _dbSet.Where(expression).Select(select).ToArrayAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {

            await _dbTransaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {

            await _dbTransaction.RollbackAsync();
        }

        public async Task DisposeTransactionAsync()
        {
           await _dbTransaction.DisposeAsync();
           
        }

        public async Task<int> SaveChanggesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetFirstorDefualtAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
    }
}
