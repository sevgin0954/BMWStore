using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByYearStrategy : ICarSortStrategy
    {
        public IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c.Year);

            return sortedCars;
        }
    }
}
