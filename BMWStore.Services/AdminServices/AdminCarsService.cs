using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly ICarRepository carRepository;
        private readonly ICarOptionRepository carOptionRepository;
        private readonly IAdminPicturesService adminPicturesService;
        private readonly ISelectListItemsService selectListItemsService;
        private readonly ICarsService carsService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminCarsService(
            ICarRepository carRepository,
            ICarOptionRepository carOptionRepository,
            IAdminPicturesService adminPicturesService,
            ISelectListItemsService selectListItemsService,
            ICarsService carsService,
            IAdminDeleteService adminDeleteService)
        {
            this.carRepository = carRepository;
            this.carOptionRepository = carOptionRepository;
            this.adminPicturesService = adminPicturesService;
            this.selectListItemsService = selectListItemsService;
            this.carsService = carsService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task<AdminCarsViewModel> GetCarsViewModelAsync(
            ICarFilterStrategy filterStrategy,
            SortStrategyDirection sortDirection,
            AdminBaseCarSortStrategyType sortType,
            int pageNumber)
        {
            var sortStrategy = BaseCarSortStrategyFactory.GetStrategy<BaseCar>(sortType, sortDirection);

            var filteredCars = this.carRepository.GetFiltered(filterStrategy);
            var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredAndSortedCars);
            var cars = await this.carsService.GetCarsModelsAsync<CarConciseViewModel>(filteredAndSortedCars, pageNumber);
            var model = new AdminCarsViewModel()
            {
                Cars = cars,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return model;
        }

        public async Task CreateCarAsync<TCar>(AdminCarBindingModel model) where TCar : BaseCar
        {
            var dbCar = Mapper.Map<TCar>(model);
            await this.adminPicturesService.UpdateCarPicturesAsync(dbCar, model.Pictures);

            this.carRepository.Add(dbCar);

            var rowsAffected = await this.carRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task SetEditBindingModelPropertiesAsync(AdminCarBindingModel model)
        {
            var allOptions = model.CarOptions;
            var carId = model.Id;
            var carModel = await this.carRepository
                    .GetAll()
                    .Where(c => c.Id == carId)
                    .To<AdminCarBindingModel>()
                    .FirstAsync();

            Mapper.Map(carModel, model);
            var selectedOptions = carModel.CarOptions;

            this.selectListItemsService.SelectItemsWithValues(model.Engines, model.SelectedEngineId);
            this.selectListItemsService.SelectItemsWithValues(model.FuelTypes, model.SelectedFuelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.ModelTypes, model.SelectedModelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.Series, model.SelectedSeriesId);
            this.selectListItemsService.SelectItemsWithValues(allOptions, selectedOptions.Select(o => o.Value).ToArray());

            model.CarOptions = allOptions;
        }

        public async Task EditCarAsync<TCar>(AdminCarBindingModel model) where TCar : BaseCar
        {
            var dbCar = await this.carRepository.Set<TCar>().FindAsync(model.Id);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(model, dbCar);
            
            if (model.Pictures.Count() > 0)
            {
                await this.adminPicturesService.UpdateCarPicturesAsync(dbCar, model.Pictures);
            }

            if (model.CarOptions.Count() > 0)
            {
                await this.carOptionRepository.RemoveAllWithCarIdAsync(model.Id);
            }

            await this.carOptionRepository.CompleteAsync();
        }

        public async Task DeleteAsync(string carId)
        {
            await this.adminDeleteService.DeleteAsync<BaseCar>(carId);
        }
    }
}
