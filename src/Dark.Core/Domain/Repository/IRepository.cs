using Dark.Core.DI;
using Dark.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Repository
{
    public interface IRepository<T> : ITransientDependency where T : IEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
        IQueryable<T> Query(Expression<Func<T, bool>> where);

        /// <summary>
        /// 异步
        /// </summary>
        /// <param name="entity"></param>
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task CommitAsync();
    }
}
