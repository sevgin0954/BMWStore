using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class CarSortStrategyFactory
    {
        public static ICarSortStrategy GetStrategy(CarSortStrategyType sortType, SortStrategyDirection sortDirection)
        {
            switch (sortType)
            {
                case CarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByConditionStrategy();
                case CarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByConditionDescStrategy();
                case CarSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByNameStrategy();
                case CarSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByNameDescStrategy();
                case CarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy();
                case CarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy();
                case CarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case CarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                case CarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case CarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
