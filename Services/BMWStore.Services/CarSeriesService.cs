using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarSeriesService : ICarSeriesService
    {
        public async Task<ICollection<FilterTypeServiceModel>> GetSeriesFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var seriesModels = await cars
                .GroupBy(c => new { Name = c.Series.Name })
                .Select(c => new FilterTypeServiceModel()
                {
                    Value = c.Key.Name,
                    Text = c.Key.Name,
                    CarsCount = c.Count()
                })
                .ToListAsync();

            return seriesModels;
        }
    }
}