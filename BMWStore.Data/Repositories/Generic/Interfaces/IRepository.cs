using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Generic.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(params TEntity[] entities);
        Task<int> CountAllAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(string id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Remove(TEntity entity);
        void RemoveRange(params TEntity[] entities);
    }
}