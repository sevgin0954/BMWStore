using BMWStore.Models.CarModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateNewCar(AdminNewCarCreateBindingModel model);
        Task CreateUsedCar(AdminNewCarCreateBindingModel model);
        Task DeleteCarAsync(string carId);
        Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model);
        Task EditNewCarAsync(AdminCarEditBindingModel model);
        Task EditUsedCarAsync(AdminCarEditBindingModel model);
    }
}
