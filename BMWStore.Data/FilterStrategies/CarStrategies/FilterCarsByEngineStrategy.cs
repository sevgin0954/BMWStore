using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByEngineStrategy : ICarFilterStrategy
    {
        private readonly string engineId;

        public FilterCarsByEngineStrategy(string engineId)
        {
            this.engineId = engineId;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c => c.EngineId == this.engineId);

            return filteredCars;
        }
    }
}
