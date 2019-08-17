using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.SortStrategies.OptionStrategies;
using BMWStore.Data.SortStrategies.OptionStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public static class OptionSortStrategyFactory
    {
        public static IOptionSortStrategy GetStrategy(
            OptionSortStrategyType sortStrategyType,
            SortStrategyDirection sortDirection)
        {
            switch (sortStrategyType)
            {
                case OptionSortStrategyType.Cars when sortDirection == SortStrategyDirection.Ascending:
                    return new SortOptionsByPredicateStrategy<int>(o => o.CarsOptions.Count);
                case OptionSortStrategyType.Cars when sortDirection == SortStrategyDirection.Descending:
                    return new SortOptionsByPredicateDescStrategy<int>(o => o.CarsOptions.Count);
                case OptionSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortOptionsByPredicateStrategy<string>(o => o.Name);
                case OptionSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortOptionsByPredicateDescStrategy<string>(o => o.Name);
                case OptionSortStrategyType.OptionType when sortDirection == SortStrategyDirection.Ascending:
                    return new SortOptionsByPredicateStrategy<string>(o => o.OptionType.Name);
                case OptionSortStrategyType.OptionType when sortDirection == SortStrategyDirection.Descending:
                    return new SortOptionsByPredicateDescStrategy<string>(o => o.OptionType.Name);
                case OptionSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortOptionsByPredicateStrategy<decimal>(o => o.Price);
                case OptionSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortOptionsByPredicateDescStrategy<decimal>(o => o.Price);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
