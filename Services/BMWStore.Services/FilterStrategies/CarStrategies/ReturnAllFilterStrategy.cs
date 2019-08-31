using System.Linq;
using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.CarStrategies
{
    public class ReturnAllFilterStrategy : ICarFilterStrategy
    {
        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            return cars;
        }
    }
}
