using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
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
        private readonly IReadService readService;

        public CarsService(
            SignInManager<User> signInManager, 
            ICarRepository carRepository, 
            IReadService readService)
        {
            this.signInManager = signInManager;
            this.carRepository = carRepository;
            this.readService = readService;
        }

        public async Task<TModel> GetByIdAsync<TModel>(string carId) where TModel : class
        {
            var carModel = await this.readService.GetModelByIdAsync<TModel, BaseCar>(carId);

            return carModel;
        }

        public async Task<CarViewModel> GetCarViewModelAsync(string carId, ClaimsPrincipal user)
        {
            var car = this.carRepository
                .Find(c => c.Id == carId);
            var models = await this.GetCarScheduleViewModelAsync<CarViewModel>(car, user, 1);
            var carModel = models.FirstOrDefault();
            DataValidator.ValidateNotNull(carModel, new ArgumentException(ErrorConstants.IncorrectId));

            return carModel;
        }

        public async Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(IQueryable<BaseCar> cars)
        {
            var models = await cars
                .To<TModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(
            IQueryable<BaseCar> cars,
            int pageNumber) where TModel : class
        {
            var models = await this.carRepository.GetAll()
                .To<TModel>()
                .GetFromPage(pageNumber)
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<TModel>> GetCarScheduleViewModelAsync<TModel>(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
            where TModel : BaseCarScheduleTestDriveViewModel
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