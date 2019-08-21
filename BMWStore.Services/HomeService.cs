﻿using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Models.HomeModels.ViewModels;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class HomeService : IHomeService
    {
        private readonly ICarRepository carRepository;
        private readonly ICarYearService carYearService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;

        public HomeService(
            ICarRepository carRepository,
            ICarYearService carYearService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService)
        {
            this.carRepository = carRepository;
            this.carYearService = carYearService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
        }

        public async Task<HomeViewModel> GetHomeModelAsync()
        {
            var allCars = this.carRepository.GetAll();

            var carYears = await this.carYearService.GetYearFilterModelsAsync(allCars);
            var carModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(allCars);
            var carPrices = await this.carPriceService.GetPriceFilterModelsAsync(allCars);

            var model = new HomeViewModel()
            {
                Years = carYears,
                ModelTypes = carModels,
                CarPrices = carPrices
            };

            return model;
        }
    }
}
