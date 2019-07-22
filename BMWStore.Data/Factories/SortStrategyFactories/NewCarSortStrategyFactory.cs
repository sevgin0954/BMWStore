using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class NewCarSortStrategyFactory
    {
        public static ICarSortStrategy GetStrategy(NewBaseCarSortStrategyType sortType, SortStrategyDirection sortDirection)
        {
            switch (sortType)
            {
                case NewBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy();
                case NewBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy();
                case NewBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case NewBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                case NewBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy();
                case NewBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
