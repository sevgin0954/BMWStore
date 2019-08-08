using BMWStore.Common.Helpers;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsService : ICarsService
    {
        private readonly SignInManager<User> signInManager;

        public CarsService(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(
            IQueryable<BaseCar> cars,
            int pageNumber) where TModel : class
        {
            var models = await cars
                .GetFromPage(pageNumber)
                .To<TModel>()
                .ToArrayAsync();

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
