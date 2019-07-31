using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEnginesService
    {
        Task CreateNewEngineAsync(AdminEngineCreateBindingModel model);
        Task<IEnumerable<EngineViewModel>> GetAllAsync();
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task EditAsync(AdminEngineEditBindingModel model);
        Task DeleteAsync(string engineId);
        Task SetEditBindingModelPropertiesAsync(AdminEngineEditBindingModel model);
    }
}
