using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.EngineModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        Task CreateEngineAsync(AdminEngineCreateBindingModel model);
        Task<AdminEnginesViewModel> GetEnginesViewModelAsync(int pageNumber, IEngineSortStrategy engineSortStrategy);
        Task EditAsync(AdminEngineEditBindingModel model);
        Task SetEditBindingModelPropertiesAsync(AdminEngineEditBindingModel model);
        Task DeleteAsync(string engineId);
    }
}
