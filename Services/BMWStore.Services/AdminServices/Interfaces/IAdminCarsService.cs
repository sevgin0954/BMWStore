using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateNewAsync<TCar>(CarServiceModel model) where TCar : BaseCar, new();
        Task DeleteAsync(string carId);
        Task EditAsync<TCar>(CarServiceModel model) where TCar : BaseCar;
    }
}
