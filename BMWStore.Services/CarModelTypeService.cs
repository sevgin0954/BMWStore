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
                .GroupBy(c => new { Name = c.ModelType.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Name,
                    Text = $"{c.Key.Name} ({c.Count()})"
                })
                .ToListAsync();

            return modelTypeModels;
        }
    }
}
