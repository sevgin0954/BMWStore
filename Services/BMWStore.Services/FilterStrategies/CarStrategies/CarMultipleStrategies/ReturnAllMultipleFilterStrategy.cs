using System.Linq;
using BMWStore.Services.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.CarStrategies.CarMultipleStrategies
{
    public class ReturnAllMultipleFilterStrategy : ICarMultipleFilterStrategy
    {
        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            return cars;
        }
    }
}
