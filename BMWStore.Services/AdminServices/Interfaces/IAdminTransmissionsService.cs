using BMWStore.Models.TransmissionsModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTransmissionsService
    {
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task CreateNewTransmissionAsync(AdminTransmissionsCreateBindingModel model);
    }
}
