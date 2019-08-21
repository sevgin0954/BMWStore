using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsFilterTypesService : ICarsFilterTypesService
    {
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarYearService carYearService;
        private readonly ICarSeriesService carSeriesService;
        private readonly ICarPriceService carPriceService;
        private readonly IFilterTypesService filterTypesService;

        public CarsFilterTypesService(
            ICarModelTypeService carModelTypeService,
            ICarYearService carYearService,
            ICarSeriesService carSeriesService,
            ICarPriceService carPriceService,
            IFilterTypesService filterTypesService)
        {
            this.carModelTypeService = carModelTypeService;
            this.carYearService = carYearService;
            this.carSeriesService = carSeriesService;
            this.carPriceService = carPriceService;
            this.filterTypesService = filterTypesService;
        }

        public async Task<CarsFilterViewModel> GetCarFilterModelAsync(
            IQueryable<BaseCar> allCars,
            IQueryable<BaseCar> filteredCars)
        {
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(allCars);
            var priceModels = await this.carPriceService.GetPriceFilterModelsAsync(filteredCars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModelsAsync(filteredCars);
            var yearModels = await this.carYearService.GetYearFilterModelsAsync(filteredCars);

            var model = new CarsFilterViewModel();

            this.AddAllFilterTypeModels(model);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);
            model.Series.AddRange(seriesModels);
            model.Years.AddRange(yearModels);

            return model;
        }

        private void AddAllFilterTypeModels(CarsFilterViewModel model)
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

        public void SelectModelFilterItems(CarsFilterViewModel model,
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
