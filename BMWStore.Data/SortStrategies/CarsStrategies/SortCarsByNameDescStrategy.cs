using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByNameDescStrategy : ICarSortStrategy
    {
        public IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.OrderByDescending(c => c.Name);

            return sortedCars;
        }
    }
}
