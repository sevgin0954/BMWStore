using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByConditionStrategy<TCar> : ICarSortStrategy<TCar> where TCar : BaseCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c is NewCar ? 0 : 1);

            return sortedCars;
        }
    }
}
