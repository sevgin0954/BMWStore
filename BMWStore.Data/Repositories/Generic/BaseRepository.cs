using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BMWStore.Data.Repositories.Generic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories.Generic
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(params TEntity[] entities)
        {
            this.dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task<int> CountAllAsync()
        {
            var count = await this.dbContext.Set<TEntity>()
                .CountAsync();

            return count;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var count = await this.dbContext.Set<TEntity>()
                .Where(predicate)
                .CountAsync();

            return count;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await this.dbContext.Set<TEntity>()
                .Where(predicate)
                .ToArrayAsync();

            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>().ToArray();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return this.dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await this.dbContext.Set<TEntity>()
                .ToArrayAsync();

            return result;
        }

        public TEntity GetById(string id)
        {
            return this.dbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await this.dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(params TEntity[] entities)
        {
            this.dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}