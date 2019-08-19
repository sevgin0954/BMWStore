using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies
{
    public class FilterCarsByMultipleModelTypesStrategy : ICarMultipleFilterStrategy
    {
        private readonly string[] modelTypeNames;

        public FilterCarsByMultipleModelTypesStrategy(params string[] modelTypeNames)
        {
            this.modelTypeNames = modelTypeNames;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var sortedCars = cars.Where(c => this.modelTypeNames.Any(mtn => mtn == c.ModelType.Name));

            return sortedCars;
        }
    }
}
