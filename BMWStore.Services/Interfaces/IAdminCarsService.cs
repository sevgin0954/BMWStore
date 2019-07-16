using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateNewCar(AdminCarCreateBindingModel model);
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync();
    }
}
