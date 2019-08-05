using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
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

        public AdminCarsService(
            ICarRepository carRepository,
            ICarOptionRepository carOptionRepository,
            IAdminPicturesService adminPicturesService,
            ISelectListItemsService selectListItemsService,
            ICarsService carsService)
        {
            this.carRepository = carRepository;
            this.carOptionRepository = carOptionRepository;
            this.adminPicturesService = adminPicturesService;
            this.selectListItemsService = selectListItemsService;
            this.carsService = carsService;
        }

        public async Task<AdminCarsViewModel> GetCarsViewModelAsync(
            string id,
            SortStrategyDirection sortDirection,
            AdminBaseCarSortStrategyType sortType,
            AdminBaseCarFilterStrategy filter,
            int pageNumber)
        {
            var sortStrategy = BaseCarSortStrategyFactory.GetStrategy<BaseCar>(sortType, sortDirection);
            var filterStrategy = AdminCarFilterStrategyFactory.GetStrategy(filter, id);

            var filteredCars = this.carRepository.GetFiltered(filterStrategy);
            var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

            var totalPagesCount = await PaginationHelper.CalculateTotalPagesCount(filteredAndSortedCars);

            var cars = await this.carsService.GetAllCarsAsync(filteredAndSortedCars, pageNumber);
            var model = new AdminCarsViewModel()
            {
                Cars = cars,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = (int)totalPagesCount
            };

            return model;
        }

        public async Task CreateCarAsync<TCar>(AdminCarCreateBindingModel model) where TCar : BaseCar
        {
            var dbCar = Mapper.Map<TCar>(model);
            await this.adminPicturesService.UpdateCarPicturesAsync(dbCar, model.Pictures);

            this.carRepository.Add(dbCar);

            var rowsAffected = await this.carRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task DeleteCarAsync(string carId)
        {
            var dbCar = await this.carRepository.GetByIdAsync(carId);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            this.carRepository.Remove(dbCar);

            var rowsAffected = await this.carRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model)
        {
            var allOptions = model.CarOptions;
            var carId = model.Id;
            var carModel = await this.carRepository
                    .GetAll()
                    .Where(c => c.Id == carId)
                    .To<AdminCarEditBindingModel>()
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

        public async Task EditCarAsync<TCar>(AdminCarEditBindingModel model) where TCar : BaseCar
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
    }
}
