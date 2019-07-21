using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByConditionStrategy : ICarSortStrategy
    {
        public IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c is NewCar ? 0 : 1);

            return sortedCars;
        }
    }
}
