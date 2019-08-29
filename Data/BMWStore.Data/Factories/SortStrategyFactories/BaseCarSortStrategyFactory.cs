using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class BaseCarSortStrategyFactory
    {
        public static ICarSortStrategy<TCar> GetStrategy<TCar>(
            BaseCarSortStrategyType sortType,
            SortStrategyDirection sortDirection) where TCar : BaseCar
        {
            switch (sortType)
            {
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, decimal>(c => c.Price);
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, decimal>(c => c.Price);

                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, string>(c => c.Year);
                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, string>(c => c.Year);

                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, int>(c => c.WarrantyMonthsLeft);
                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, int>(c => c.WarrantyMonthsLeft);

                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
