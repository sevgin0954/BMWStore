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
                .GroupBy(c => new { Value = c.Series.Id, Text = c.Series.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Value,
                    Text = $"{c.Key.Text} ({c.Count()})"
                })
                .ToListAsync();

            return seriesModels;
        }
    }
}