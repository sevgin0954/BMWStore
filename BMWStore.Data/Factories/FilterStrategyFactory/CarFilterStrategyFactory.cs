﻿using BMWStore.Common.Constants;
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
            int? startYear,
            decimal? minPrice,
            decimal? maxPrice,
            string series,
            IEnumerable<string> modelTypes)
        {
            var strategies = new List<ICarFilterStrategy>();

            if (startYear != null)
            {
                var yearStrategy = CreateYearStrategy((int)startYear);
                strategies.Add(yearStrategy);
            }

            if (minPrice != null && maxPrice != null)
            {
                var priceStrategy = CreatePriceRangeStrategy((decimal)minPrice, (decimal)maxPrice);
                strategies.Add(priceStrategy);
            }

            if (series != null)
            {
                var seriesStrategy = CreateSeriesStrategy(series);
                strategies.Add(seriesStrategy);
            }

            if (modelTypes != null && modelTypes.Count() > 0)
            {
                var modelTypeStrategy = CreateModelTypeStrategy(modelTypes.ToArray());
                strategies.Add(modelTypeStrategy);
            }

            return strategies;
        }

        private static ICarFilterStrategy CreateYearStrategy(int startYear)
        {
            DataValidator.ValidateYearString(startYear.ToString());
            var yearStrategy = new FilterCarsByYearStrategy(startYear);

            return yearStrategy;
        }

        private static ICarFilterStrategy CreatePriceRangeStrategy(decimal minPrice, decimal maxPrice)
        {
            DataValidator.ValidateMinPrice(minPrice, EntitiesConstants.MinPrice);
            DataValidator.ValidateMaxPrice(maxPrice, EntitiesConstants.CarMaxPrice);
            var priceStrategy = new FilterCarsByPriceRangeStrategy(minPrice, maxPrice);

            return priceStrategy;
        }

        private static ICarFilterStrategy CreateSeriesStrategy(string series)
        {
            var filterStrategy = new FilterCarsBySeriesStrategy(series);

            return filterStrategy;
        }

        private static ICarFilterStrategy CreateModelTypeStrategy(params string[] modelTypes)
        {
            var modelTypeStrategy = new FilterCarsByModelTypeStrategy(modelTypes);

            return modelTypeStrategy;
        }
    }
}
