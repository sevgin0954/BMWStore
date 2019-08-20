using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.EngineModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        Task CreateEngineAsync(EngineBindingModel model);
        Task<AdminEnginesViewModel> GetEnginesViewModelAsync(int pageNumber, IEngineSortStrategy engineSortStrategy);
        Task<EngineBindingModel> GetEditModelAsync(string engineId);
        Task EditAsync(EngineBindingModel model);
        Task DeleteAsync(string engineId);
    }
}
