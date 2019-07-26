using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsInvertoryService : ICarsInvertoryService
    {
        private readonly ICarYearService carYearService;
        private readonly ICarSeriesService carSeriesService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;
        private readonly IFilterTypesService filterTypesService;

        public CarsInvertoryService(
            ICarYearService carYearService,
            ICarSeriesService carSeriesService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService,
            IFilterTypesService filterTypesService)
        {
            this.carYearService = carYearService;
            this.carSeriesService = carSeriesService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
            this.filterTypesService = filterTypesService;
        }

        public async Task<CarsInvertoryViewModel> GetInvertoryBindingModel(
            IQueryable<BaseCar> cars,
            Enum sortStrategy,
            SortStrategyDirection sortDirection)
        {
            var carModels = await cars.To<CarConciseViewModel>().ToArrayAsync();

            // TODO: Use caching
            var yearModels = await this.carYearService.GetYearFilterModels(cars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModels(cars);
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModels(cars);
            var priceModels = await this.carPriceService.GetPriceFilterModels(carModels);

            var model = new CarsInvertoryViewModel();

            var allFilterModel = this.AddAllFilterTypeModel();
            model.Years.Add(allFilterModel);
            model.Series.Add(allFilterModel);
            model.Prices.Add(allFilterModel);

            model.Years.AddRange(yearModels);
            model.Series.AddRange(seriesModels);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);

            model.Cars = carModels;

            model.SortStrategyType = sortStrategy;
            model.SortStrategyDirection = sortDirection;

            return model;
        }

        private FilterTypeBindingModel AddAllFilterTypeModel()
        {
            var allModel = new FilterTypeBindingModel()
            {
                Text = WebConstants.AllFilterTypeModelText,
                Value = WebConstants.AllFilterTypeModelValue
            };

            return allModel;
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
