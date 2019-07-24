using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(ICarSortStrategy sortStrategy);
        Task<IEnumerable<CarConciseViewModel>> GetAllNewCarsAsync(
            ICarSortStrategy sortStrategy, 
            params ICarFilterStrategy[] filterStrategies);
    }
}
