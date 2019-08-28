using AutoMapper;
using BMWStore.Common.Enums;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Services.Interfaces;
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

        public async Task<HomeSearchBindingModel> GetSearchModelAsync(IQueryable<BaseCar> cars, CarType carType)
        {
            var carsOfType = cars.Where(c => c.GetType().Name == carType.ToString());

            var carYearServiceModels = await this.carYearService.GetYearFilterModelsAsync(carsOfType);
            var carYearBindingModel = Mapper.Map<IEnumerable<FilterTypeBindingModel>>(carYearServiceModels);

            var carModelServiceModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(carsOfType);
            var carModelBindingModels = Mapper.Map<IEnumerable<FilterTypeBindingModel>>(carModelServiceModels);

            var carPriceServiceModels = await this.carPriceService.GetPriceFilterModelsAsync(carsOfType);
            var carPriceBindingModels = Mapper.Map<IEnumerable<FilterTypeBindingModel>>(carPriceServiceModels);

            var filterTypeServiceModels = await this.carInventoriesService.GetInventoryFilterModelsAsync(cars);
            var filterTypeBindingModel = Mapper
                .Map<IEnumerable<FilterTypeBindingModel>>(filterTypeServiceModels);
            FilterTypeHelper.SelectFilterTypes(filterTypeBindingModel, carType.ToString());

            var model = new HomeSearchBindingModel()
            {
                CarTypes = filterTypeBindingModel,
                Years = carYearBindingModel,
                ModelTypes = carModelBindingModels,
                PriceRanges = carPriceBindingModels
            };

            return model;
        }
    }
}
