using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Services.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        IQueryable<BaseCarServiceModel> GetCars(
            ICarSortStrategy<BaseCar> sortStrategy,
            params ICarFilterStrategy[] filterStrategies);
        Task<CarServiceModel> GetByIdAsync(string carId);
        IQueryable<TCar> GetFiltered<TCar>(params ICarFilterStrategy[] filterStrategies)
            where TCar : BaseCar;
        Task<TModel> GetCarTestDriveModelById<TModel>(string id, ClaimsPrincipal user)
             where TModel : BaseCarTestDriveServiceModel;
        IQueryable<TModel> GetCarTestDriveModel<TModel>(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber) where TModel : BaseCarTestDriveServiceModel;
    }
}