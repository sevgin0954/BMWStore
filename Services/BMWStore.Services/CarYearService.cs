using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarYearService : ICarYearService
    {
        public async Task<ICollection<FilterTypeServiceModel>> GetYearFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var yearModels = await cars
                .GroupBy(c => c.Year)
                .Select(c => new FilterTypeServiceModel()
                {
                    Value = c.Key,
                    Text = c.Key,
                    CarsCount = c.Count()
                })
                .ToListAsync();

            return yearModels;
        }
    }
}
