using BMWStore.Models.ModelTypeModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminModelTypesService
    {
        Task CreateNewModelType(AdminModelTypeCreateBidningModel model);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
    }
}
