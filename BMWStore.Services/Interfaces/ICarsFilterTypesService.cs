using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsFilterTypesService
    {
        Task<CarsFilterViewModel> GetCarFilterModel(
            IQueryable<BaseCar> allCars,
            IQueryable<BaseCar> filteredCars);
        void SelectModelFilterItems(CarsFilterViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes);
    }
}
