using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class ReturnAllFilterStrategy : ICarFilterStrategy
    {
        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            return cars;
        }
    }
}
