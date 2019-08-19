using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInventoryModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsInventoryService
    {
        Task<CarsInventoryViewModel> GetInventoryViewModelAsync(
            ICarMultipleFilterStrategy multipleFilterStrategy,
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber);
        void SelectModelFilterItems(CarsInventoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes);
    }
}
