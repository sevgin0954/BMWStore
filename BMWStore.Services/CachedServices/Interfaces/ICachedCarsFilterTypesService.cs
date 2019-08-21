using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.CachedServices.Interfaces
{
    public interface ICachedCarsFilterTypesService
    {
        Task<CarsFilterViewModel> GetCachedCarFilterModelAsync(
            string cacheKey,
            IQueryable<BaseCar> cars,
            IQueryable<BaseCar> filteredByMultipleCars);
    }
}
