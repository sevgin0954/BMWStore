using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Generic.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(params TEntity[] entities);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAllAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(params object[] keyValues);
        IQueryable<TEntity> GetAll();
        void Remove(TEntity entity);
        void RemoveRange(params TEntity[] entities);
    }
}