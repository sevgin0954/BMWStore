using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsInvertoryService
    {
        Task<CarsInvertoryViewModel> GetInvertoryBindingModel(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber);
        void SelectModelFilterItems(CarsInvertoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes);
    }
}
