using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.FuelTypeModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        Task CreateNewFuelTypeAsync(FuelTypeBindingModel model);
        Task<AdminFuelTypesViewModel> GetFuelTypesViewModelAsync(int pageNumber);
        Task<FuelTypeBindingModel> GetEditingModelAsync(string fuelTypeId);
        Task EditAsync(FuelTypeBindingModel model);
        Task DeleteAsync(string fuelTypeId);
    }
}
