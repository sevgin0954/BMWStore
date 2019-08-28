using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarModelTypeService : ICarModelTypeService
    {
        public async Task<ICollection<FilterTypeServiceModel>> GetModelTypeFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var modelTypeModels = await cars
                .GroupBy(c => new { Name = c.ModelType.Name })
                .Select(c => new FilterTypeServiceModel()
                {
                    Value = c.Key.Name,
                    Text = c.Key.Name,
                    CarsCount = c.Count()
                })
                .ToListAsync();

            return modelTypeModels;
        }
    }
}
