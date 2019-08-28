using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsService : ICarsService
    {
        private readonly SignInManager<User> signInManager;
        private readonly ICarRepository carRepository;

        public CarsService(
            SignInManager<User> signInManager, 
            ICarRepository carRepository)
        {
            this.signInManager = signInManager;
            this.carRepository = carRepository;
        }

        public async Task<CarServiceModel> GetByIdAsync(string carId)
        {
            var carModel = await this.carRepository
                .FindAll(carId)
                .To<CarServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(carModel, new ArgumentException(ErrorConstants.IncorrectId));

            return carModel;
        }

        public IQueryable<CarConciseViewModel> GetCars(
            ICarSortStrategy<BaseCar> sortStrategy,
            params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.carRepository.GetFiltered(filterStrategies);
            var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

            var carModels = filteredAndSortedCars.To<CarConciseViewModel>();

            return carModels;
        }


        public async Task<CarServiceModel> GetCarTestDriveModelById(string id, ClaimsPrincipal user)
        {
            var car = this.carRepository
                .Find(c => c.Id == id);
            var models = await this.GetCarTestDriveModelAsync<CarServiceModel>(car, user, 1);
            var carModel = models.FirstOrDefault();
            DataValidator.ValidateNotNull(carModel, new ArgumentException(ErrorConstants.IncorrectId));

            return carModel;
        }

        public async Task<IEnumerable<TModel>> GetCarTestDriveModelAsync<TModel>(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber) where TModel : BaseCarTestDriveServiceModel
        {
            var isUserSignedIn = this.signInManager.IsSignedIn(user);

            var models = await cars
                .GetFromPage(pageNumber)
                .To<TModel>(new { isUserSignedIn })
                .ToArrayAsync();

            return models;
        }
    }
}