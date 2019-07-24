using System.Linq;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByPriceRangeStrategy : ICarFilterStrategy
    {
        private readonly decimal minPrice;
        private readonly decimal maxPrice;

        public FilterCarsByPriceRangeStrategy(decimal minPrice, decimal maxPrice)
        {
            DataValidator.ValidateMinPrice(minPrice, EntitiesConstants.MinPrice);
            DataValidator.ValidateMaxPrice(maxPrice, EntitiesConstants.CarMaxPrice);
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice);

            return filteredCars;
        }
    }
}
