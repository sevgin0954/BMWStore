using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByOptionStrategy : ICarFilterStrategy
    {
        private readonly string optionId;

        public FilterCarsByOptionStrategy(string optionId)
        {
            this.optionId = optionId;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c => c.Options.Any(o => o.OptionId == this.optionId));

            return filteredCars;
        }
    }
}
