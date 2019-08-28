using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarSeriesService
    {
        Task<ICollection<FilterTypeServiceModel>> GetSeriesFilterModelsAsync(IQueryable<BaseCar> cars);
    }
}
