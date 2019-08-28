using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarInventoriesService : ICarInventoriesService
    {
        public async Task<ICollection<FilterTypeServiceModel>> GetInventoryFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var filterTypesModels = await cars
                .GroupBy(c => new { TypeName = c.GetType().Name })
                .Select(c => new FilterTypeServiceModel()
                {
                    Value = c.Key.TypeName,
                    Text = c.Key.TypeName,
                    CarsCount = c.Count()
                })
                .ToListAsync();

            return filterTypesModels;
        }
    }
}
