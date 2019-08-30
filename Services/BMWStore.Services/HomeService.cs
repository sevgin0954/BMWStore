using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class HomeService : IHomeService
    {
        private readonly ICarInventoriesService carInventoriesService;
        private readonly ICarYearService carYearService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;

        public HomeService(
            ICarInventoriesService carInventoriesService,
            ICarYearService carYearService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService)
        {
            this.carInventoriesService = carInventoriesService;
            this.carYearService = carYearService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
        }

        public async Task<HomeSearchServiceModel> GetSearchModelAsync(IQueryable<BaseCar> cars, CarType carType)
        {
            var filterTypeServiceModels = await this.carInventoriesService.GetInventoryFilterModelsAsync(cars);
            var carsOfType = cars.Where(c => c.GetType().Name == carType.ToString());
            var carYearServiceModels = await this.carYearService.GetYearFilterModelsAsync(carsOfType);
            var carModelServiceModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(carsOfType);
            var carPriceServiceModels = await this.carPriceService.GetPriceFilterModelsAsync(carsOfType);

            var model = new HomeSearchServiceModel()
            {
                CarTypes = filterTypeServiceModels,
                Years = carYearServiceModels,
                ModelTypes = carModelServiceModels,
                PriceRanges = carPriceServiceModels
            };

            return model;
        }
    }
}