using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using System;
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
            Enum sortStrategy,
            SortStrategyDirection sortDirection,
            ClaimsPrincipal user);
        void SelectModelFilterItems(CarsInvertoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes);
    }
}
