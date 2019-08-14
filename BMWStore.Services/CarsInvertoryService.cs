using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMWStore.Common.Helpers;

namespace BMWStore.Services
{
    public class CarsInvertoryService : ICarsInvertoryService
    {
        private readonly ICarYearService carYearService;
        private readonly ICarSeriesService carSeriesService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;
        private readonly IFilterTypesService filterTypesService;
        private readonly ICarsService carsService;

        public CarsInvertoryService(
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

        public async Task<CarsInvertoryViewModel> GetInvertoryViewModelAsync(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
        {
            var totalCarPages = await PaginationHelper.CountTotalPagesCountAsync(cars);

            var carModels = await this.carsService.GetCarsInvertoryViewModelAsync(cars, user, pageNumber);

            var yearModels = await this.carYearService.GetYearFilterModelsAsync(cars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModelsAsync(cars);
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(cars);
            var priceModels = await this.carPriceService.GetPriceFilterModelsAsync(carModels);

            var model = new CarsInvertoryViewModel()
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

        private void AddAllFilterTypeModels(CarsInvertoryViewModel model)
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

        public void SelectModelFilterItems(CarsInvertoryViewModel model,
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
