using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortUsedCarsByMileageDescStrategy<TCar> : ICarSortStrategy<TCar> where TCar : UsedCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderByDescending(c => c.Mileage);

            return sortedCars;
        }
    }
}
