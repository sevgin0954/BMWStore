using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarSeriesService
    {
        Task<ICollection<FilterTypeBindingModel>> GetSeriesFilterModels(IQueryable<BaseCar> cars);
    }
}
