using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
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

        public CarsFilterTypesService(
            ICarModelTypeService carModelTypeService,
            ICarYearService carYearService,
            ICarSeriesService carSeriesService,
            ICarPriceService carPriceService)
        {
            this.carModelTypeService = carModelTypeService;
            this.carYearService = carYearService;
            this.carSeriesService = carSeriesService;
            this.carPriceService = carPriceService;
        }

        public async Task<CarsFilterServiceModel> GetCarFilterModelAsync(
            IQueryable<BaseCar> allCars,
            IQueryable<BaseCar> filteredCars)
        {
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(allCars);
            var priceModels = await this.carPriceService.GetPriceFilterModelsAsync(filteredCars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModelsAsync(filteredCars);
            var yearModels = await this.carYearService.GetYearFilterModelsAsync(filteredCars);

            var model = new CarsFilterServiceModel();

            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);
            model.Series.AddRange(seriesModels);
            model.Years.AddRange(yearModels);

            return model;
        }
    }
}