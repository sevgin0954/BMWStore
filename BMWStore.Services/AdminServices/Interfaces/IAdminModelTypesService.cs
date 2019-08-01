using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminModelTypesService
    {
        Task CreateNewModelType(AdminModelTypeCreateBidningModel model);
        Task<IEnumerable<ModelTypeViewModel>> GetAllAsync();
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task DeleteAsync(string modelTypeId);
    }
}
