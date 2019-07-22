using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class CarSortStrategyFactory
    {
        public static ICarSortStrategy GetStrategy(BaseCarSortStrategyType sortType, SortStrategyDirection sortDirection)
        {
            switch (sortType)
            {
                case BaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByConditionStrategy();
                case BaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByConditionDescStrategy();
                case BaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByNameStrategy();
                case BaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByNameDescStrategy();
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy();
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy();
                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
