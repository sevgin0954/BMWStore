using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarSeriesService : ICarSeriesService
    {
        public async Task<ICollection<FilterTypeBindingModel>> GetSeriesFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var seriesModels = await cars
                .GroupBy(c => new { Name = c.Series.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Name,
                    Text = $"{c.Key.Name} ({c.Count()})"
                })
                .ToListAsync();

            return seriesModels;
        }
    }
}