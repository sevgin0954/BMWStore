using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        IQueryable<EngineServiceModel> GetAll();
        IQueryable<EngineServiceModel> GetSorted(IEngineSortStrategy sortStrategy, int pageNumber);
        Task CreateNewAsync(EngineServiceModel model);
        Task<TModel> GetEngineByIdAsync<TModel>(string id)
            where TModel : class;
        Task EditAsync(EngineServiceModel model);
        Task DeleteAsync(string engineId);
    }
}
