using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.CarStrategies;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Data.Factories.FilterStrategyFactory
{
    public class CarFilterStrategyFactory
    {
        public static IEnumerable<ICarFilterStrategy> GetStrategies(
            string year,
            decimal? minPrice,
            decimal? maxPrice,
            string series)
        {
            var strategies = new List<ICarFilterStrategy>();

            if (year != WebConstants.AllFilterTypeModelValue)
            {
                var yearStrategy = CreateYearStrategy(int.Parse(year));
                strategies.Add(yearStrategy);
            }

            if (minPrice != null && maxPrice != null)
            {
                var priceStrategy = CreatePriceRangeStrategy((decimal)minPrice, (decimal)maxPrice);
                strategies.Add(priceStrategy);
            }

            if (series != WebConstants.AllFilterTypeModelValue)
            {
                var seriesStrategy = CreateSeriesStrategy(series);
                strategies.Add(seriesStrategy);
            }

            return strategies;
        }

        private static ICarFilterStrategy CreateYearStrategy(int startYear)
        {
            DataValidator.ValidateYearString(startYear.ToString());
            var yearStrategy = new FilterCarsByPredicateStrategy(c => int.Parse(c.Year) == startYear);

            return yearStrategy;
        }

        private static ICarFilterStrategy CreatePriceRangeStrategy(decimal minPrice, decimal maxPrice)
        {
            DataValidator.ValidateMinPrice(minPrice, EntitiesConstants.MinPrice);
            DataValidator.ValidateMaxPrice(maxPrice, EntitiesConstants.CarMaxPrice);
            var priceStrategy = new FilterCarsByPredicateStrategy(c => c.Price >= minPrice && c.Price <= maxPrice);

            return priceStrategy;
        }

        private static ICarFilterStrategy CreateSeriesStrategy(string seriesName)
        {
            var filterStrategy = new FilterCarsByPredicateStrategy(c => c.Series.Name == seriesName);

            return filterStrategy;
        }
    }
}
