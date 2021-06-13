using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface IRepository<T>  where T : BaseEntity
    {
        T Create(T entity);


        T Update(T entity);


        void Delete(T entity);

        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> expression);

        Task<T> GetFirstorDefualtAsync(Expression<Func<T, bool>> expression);
        Task<ICollection<Tselect>> GetAsync<Tselect>(Expression<Func<T, bool>> expression, Expression<Func<T, Tselect>> select);
       


        Task BeginTransactionAsync();


        Task CommitAsync();


        Task RollbackAsync();


        Task DisposeTransactionAsync();
        Task<int> SaveChanggesAsync();


    }
}
