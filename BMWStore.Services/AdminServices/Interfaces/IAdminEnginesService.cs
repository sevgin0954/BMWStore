using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        Task CreateEngineAsync(AdminEngineCreateBindingModel model);
        Task<IEnumerable<EngineViewModel>> GetAllAsync();
        Task EditAsync(AdminEngineEditBindingModel model);
        Task SetEditBindingModelPropertiesAsync(AdminEngineEditBindingModel model);
    }
}
