using System.Linq;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.CarsStrategies
{
    public class SortCarsByPriceDescStrategy<TCar> : ICarSortStrategy<TCar> where TCar : BaseCar
    {
        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderByDescending(c => c.Price);

            return sortedCars;
        }
    }
}
