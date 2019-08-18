using BMWStore.Common.Constants;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
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

        public CarsService(SignInManager<User> signInManager, ICarRepository carRepository, IReadService readService)
        {
            this.signInManager = signInManager;
            this.carRepository = carRepository;
            this.readService = readService;
        }

        public async Task<CarViewModel> GetCarViewModelAsync(string carId)
        {
            var model = await this.carRepository
                .Find(c => c.Id == carId)
                .To<CarViewModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
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
            var models = await this.readService.GetAllAsync<TModel, BaseCar>(cars, pageNumber);

            return models;
        }

        public async Task<IEnumerable<CarInvertoryConciseViewModel>> GetCarsInvertoryViewModelAsync(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
        {
            var isUserSignedIn = this.signInManager.IsSignedIn(user);

            var models = await cars
                .GetFromPage(pageNumber)
                .To<CarInvertoryConciseViewModel>(new { isUserSignedIn })
                .ToArrayAsync();

            return models;
        }
    }
}
