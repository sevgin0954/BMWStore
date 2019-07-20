using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateNewCar(AdminNewCarCreateBindingModel model);
        Task CreateUsedCar(AdminNewCarCreateBindingModel model);
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync();
        Task DeleteCarAsync(string carId);
        Task GetEditBindingModel(string id);
    }
}
