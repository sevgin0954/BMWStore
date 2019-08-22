using BMWStore.Common.Helpers;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<HomeSearchBindingModel> GetSearchModelAsync(IQueryable<BaseCar> cars)
        {
            var allCars = this.carRepository.GetAll();
            var carInventories = await this.carInventoriesService.GetInventoryFilterModelsAsync(allCars);
            await this.SelectInventoryAsync(carInventories, cars);

            var carYears = await this.carYearService.GetYearFilterModelsAsync(cars);
            var carModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(cars);
            var carPrices = await this.carPriceService.GetPriceFilterModelsAsync(cars);

            var model = new HomeSearchBindingModel()
            {
                CarInverntories = carInventories,
                Years = carYears,
                ModelTypes = carModels,
                Prices = carPrices
            };

            return model;
        }

        private async Task SelectInventoryAsync(IEnumerable<FilterTypeBindingModel> carInventories, IQueryable<BaseCar> cars)
        {
            var carsType = await cars.Select(c => c.GetType().Name).FirstOrDefaultAsync();
            FilterTypeHelper.SelectFilterTypes(carInventories, carsType);
        }
    }
}
