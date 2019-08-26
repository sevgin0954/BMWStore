using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task CreateNewAsync<TCar>(CarServiceModel model) where TCar : BaseCar, new();
        Task EditAsync<TCar>(CarServiceModel model) where TCar : BaseCar;
        Task DeleteAsync(string carId);
    }
}
