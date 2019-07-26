using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(ICarSortStrategy<BaseCar> sortStrategy);
    }
}
