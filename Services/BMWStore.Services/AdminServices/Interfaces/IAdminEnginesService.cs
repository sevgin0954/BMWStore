using BMWStore.Services.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        Task CreateNewAsync(EngineServiceModel model);
        Task DeleteAsync(string engineId);
        IQueryable<EngineServiceModel> GetAll();
        Task<EngineServiceModel> GetByIdAsync(string id);
        IQueryable<EngineServiceModel> GetSorted(IEngineSortStrategy sortStrategy, int pageNumber);
        Task EditAsync(EngineServiceModel model);
    }
}
