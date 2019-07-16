using BMWStore.Models.FuelTypeModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IFuelTypesService
    {
        Task CreateNewFuelTypeAsync(AdminFuelTypeCreateBindingModel model);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
    }
}
