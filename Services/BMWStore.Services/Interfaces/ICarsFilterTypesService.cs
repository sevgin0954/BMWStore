using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsFilterTypesService
    {
        Task<CarsFilterServiceModel> GetCarFilterModelAsync(
            IQueryable<BaseCar> allCars,
            IQueryable<BaseCar> filteredCars);
    }
}
