using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        Task<CarViewModel> GetCarViewModelAsync(string carId);
        Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(IQueryable<BaseCar> cars);
        Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(
            IQueryable<BaseCar> cars,
            int pageNumber) where TModel : class;
        Task<IEnumerable<CarInvertoryConciseViewModel>> GetCarsInvertoryViewModelAsync(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber);
    }
}
