using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.FuelTypeModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        Task CreateNewFuelTypeAsync(AdminFuelTypeCreateBindingModel model);
        Task<AdminFuelTypesViewModel> GetFuelTypesViewModelAsync(int pageNumber);
    }
}
