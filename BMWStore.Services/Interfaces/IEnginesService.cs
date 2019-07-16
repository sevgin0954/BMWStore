using BMWStore.Models.EngineModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IEnginesService
    {
        Task CreateNewEngineAsync(AdminEngineCreateBindingModel model);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
    }
}
