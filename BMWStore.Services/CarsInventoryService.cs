using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMWStore.Common.Helpers;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.CarInventoryModels.ViewModels;

namespace BMWStore.Services
{
    public class CarsInventoryService : ICarsInventoryService
    {
        private readonly ICarYearService carYearService;
        private readonly ICarSeriesService carSeriesService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;
        private readonly IFilterTypesService filterTypesService;
        private readonly ICarsService carsService;

        public CarsInventoryService(
            ICarYearService carYearService,
            ICarSeriesService carSeriesService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService,
            IFilterTypesService filterTypesService,
            ICarsService carsService)
        {
            this.carYearService = carYearService;
            this.carSeriesService = carSeriesService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
            this.filterTypesService = filterTypesService;
            this.carsService = carsService;
        }

        public async Task<CarsInventoryViewModel> GetInventoryViewModelAsync(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
        {
            var totalCarPages = await PaginationHelper.CountTotalPagesCountAsync(cars);

            var allCarModels = await this.carsService.GetCarsModelsAsync<CarInventoryConciseViewModel>(cars);
            var carModels = await this.carsService.GetCarsInventoryViewModelAsync(cars, user, pageNumber);

            var yearModels = await this.carYearService.GetYearFilterModelsAsync(cars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModelsAsync(cars);
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(cars);
            var priceModels = await this.carPriceService.GetPriceFilterModelsAsync(allCarModels);

            var model = new CarsInventoryViewModel()
            {
                CurrentPage = pageNumber,
                TotalPagesCount = totalCarPages
            };

            this.AddAllFilterTypeModels(model);

            model.Years.AddRange(yearModels);
            model.Series.AddRange(seriesModels);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);

            model.Cars = carModels;

            return model;
        }

        private void AddAllFilterTypeModels(CarsInventoryViewModel model)
        {
            var allFilterModel = new FilterTypeBindingModel()
            {
                Text = WebConstants.AllFilterTypeModelText,
                Value = WebConstants.AllFilterTypeModelValue
            };

            model.Years.Add(allFilterModel);
            model.Series.Add(allFilterModel);
            model.Prices.Add(allFilterModel);
        }

        public void SelectModelFilterItems(CarsInventoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes)
        {
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Years, year);
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Series, series);
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.ModelTypes, modelTypes.ToArray());
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Prices, priceRange);
        }
    }
}
