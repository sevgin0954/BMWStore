using BMWStore.Common.Enums;
using BMWStore.Common.Helpers;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class HomeService : IHomeService
    {
        private readonly ICarRepository carRepository;
        private readonly ICarInventoriesService carInventoriesService;
        private readonly ICarYearService carYearService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;

        public HomeService(
            ICarRepository carRepository,
            ICarInventoriesService carInventoriesService,
            ICarYearService carYearService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService)
        {
            this.carRepository = carRepository;
            this.carInventoriesService = carInventoriesService;
            this.carYearService = carYearService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
        }

        public async Task<HomeSearchBindingModel> GetSearchModelAsync(IQueryable<BaseCar> cars, CarType carType)
        {
            var carsOfType = cars.Where(c => c.GetType().Name == carType.ToString());

            var carYears = await this.carYearService.GetYearFilterModelsAsync(carsOfType);
            var carModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(carsOfType);
            var carPrices = await this.carPriceService.GetPriceFilterModelsAsync(carsOfType);

            var carInventories = await this.carInventoriesService.GetInventoryFilterModelsAsync(cars);
            FilterTypeHelper.SelectFilterTypes(carInventories, carType.ToString());

            var model = new HomeSearchBindingModel()
            {
                CarTypes = carInventories,
                Years = carYears,
                ModelTypes = carModels,
                PriceRanges = carPrices
            };

            return model;
        }
    }
}
