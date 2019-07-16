using BMWStore.Models.TransmissionsModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ITransmissionsService
    {
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task CreateNewTransmissionAsync(AdminTransmissionsCreateBindingModel model);
    }
}
