using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByModelTypeIdStrategy : ICarFilterStrategy
    {
        private readonly string modelTypeId;

        public FilterCarsByModelTypeIdStrategy(string modelTypeId)
        {
            this.modelTypeId = modelTypeId;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c => c.ModelTypeId == this.modelTypeId);

            return filteredCars;
        }
    }
}
