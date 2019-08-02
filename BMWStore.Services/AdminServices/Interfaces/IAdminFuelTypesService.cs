using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Models.FuelTypeModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        Task CreateNewFuelTypeAsync(AdminFuelTypeCreateBindingModel model);
        Task<IEnumerable<FuelTypeViewModel>> GetAllAsync();
        Task DeleteAsync(string fuelTypeId);
    }
}
