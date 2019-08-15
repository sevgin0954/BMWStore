using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.FuelTypeModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        Task CreateNewFuelTypeAsync(FuelTypeCreateBindingModel model);
        Task<AdminFuelTypesViewModel> GetFuelTypesViewModelAsync(int pageNumber);
        Task<FuelTypeEditBindingModel> GetEditingModelAsync(string fuelTypeId);
        Task EditAsync(FuelTypeEditBindingModel model);
        Task DeleteAsync(string fuelTypeId);
    }
}
