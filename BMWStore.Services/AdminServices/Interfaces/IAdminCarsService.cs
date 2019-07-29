using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateCarAsync<TCar>(AdminCarCreateBindingModel model) where TCar : BaseCar;
        Task DeleteCarAsync(string carId);
        Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model);
        Task EditCarAsync<TCar>(AdminCarEditBindingModel model) where TCar : BaseCar;
    }
}
