using BMWStore.Common.Helpers;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsService : ICarsService
    {
        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(
            IQueryable<BaseCar> cars,
            int pageNumber)
        {
            var models = await cars
                .GetFromPage(pageNumber)
                .Include(uc => uc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();

            return models;
        }
    }
}
