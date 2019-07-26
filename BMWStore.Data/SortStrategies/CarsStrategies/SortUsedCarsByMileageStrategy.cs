using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortUsedCarsByMileageStrategy<TCar> : ICarSortStrategy<TCar> where TCar : UsedCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c.Mileage);

            return sortedCars;
        }
    }
}
