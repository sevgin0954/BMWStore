using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByNameDescStrategy<TCar> : ICarSortStrategy<TCar> where TCar : BaseCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderByDescending(c => c.Name);

            return sortedCars;
        }
    }
}
