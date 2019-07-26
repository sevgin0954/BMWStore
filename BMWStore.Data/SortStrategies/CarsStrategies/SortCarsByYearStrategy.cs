using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByYearStrategy<TCar> : ICarSortStrategy<TCar> where TCar : BaseCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderBy(c => c.Year);

            return sortedCars;
        }
    }
}
