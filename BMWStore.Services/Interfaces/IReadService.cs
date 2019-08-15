using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IReadService
    {
        Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>()
            where TEntity : class
            where TModel : class;
        Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>(int pageNumber)
            where TEntity : class
            where TModel : class;
        Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>(IQueryable<TEntity> entities, int pageNumber)
            where TEntity : class
            where TModel : class;
    }
}
