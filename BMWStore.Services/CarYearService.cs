using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarYearService : ICarYearService
    {
        public async Task<ICollection<FilterTypeBindingModel>> GetYearFilterModels(IQueryable<BaseCar> cars)
        {
            var yearModels = await cars
                .GroupBy(c => c.Year)
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key,
                    Text = $"{c.Key} ({c.Count()})"
                })
                .ToListAsync();

            return yearModels;
        }
    }
}
