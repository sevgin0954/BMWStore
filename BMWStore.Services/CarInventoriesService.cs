using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarInventoriesService : ICarInventoriesService
    {
        public async Task<ICollection<FilterTypeBindingModel>> GetInventoryFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var filterTypesModels = await cars
                .GroupBy(c => new { TypeName = c.GetType().Name })
                .Select(c => new FilterTypeBindingModel()
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
