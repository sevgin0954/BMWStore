using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarModelTypeService : ICarModelTypeService
    {
        public async Task<ICollection<FilterTypeBindingModel>> GetModelTypeFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var modelTypeModels = await cars
                .GroupBy(c => new { Value = c.ModelType.Id, Text = c.ModelType.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Value,
                    Text = $"{c.Key.Text} ({c.Count()})"
                })
                .ToListAsync();

            return modelTypeModels;
        }
    }
}
