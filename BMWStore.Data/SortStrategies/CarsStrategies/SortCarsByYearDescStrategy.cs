using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByYearDescStrategy : ICarSortStrategy
    {
        public IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.OrderByDescending(c => c.Year);

            return sortedCars;
        }
    }
}
