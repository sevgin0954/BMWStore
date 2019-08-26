using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IReadService
    {
        Task<TModel> GetModelByIdAsync<TModel, TEntity>(string id)
            where TEntity : class
            where TModel : class;
    }
}
