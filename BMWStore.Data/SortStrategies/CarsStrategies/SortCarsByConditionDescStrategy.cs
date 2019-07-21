using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByConditionDescStrategy : ICarSortStrategy
    {
        public IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c is NewCar ? 1 : 0);

            return sortedCars;
        }
    }
}
