using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarInventoriesService
    {
        Task<ICollection<FilterTypeBindingModel>> GetInventoryFilterModelsAsync(IQueryable<BaseCar> cars);
    }
}
