using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        IQueryable<CarConciseViewModel> GetCars(
            ICarSortStrategy<BaseCar> sortStrategy,
            params ICarFilterStrategy[] filterStrategies);
        Task<CarServiceModel> GetByIdAsync(string carId);
        Task<CarServiceModel> GetCarTestDriveModelById(string id, ClaimsPrincipal user);
        Task<IEnumerable<TModel>> GetCarTestDriveModelAsync<TModel>(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber) where TModel : BaseCarTestDriveServiceModel;
    }
}