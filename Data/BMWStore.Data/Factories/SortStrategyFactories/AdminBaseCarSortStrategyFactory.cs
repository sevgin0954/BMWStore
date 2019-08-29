using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public static class AdminBaseCarSortStrategyFactory
    {
        public static ICarSortStrategy<TCar> GetStrategy<TCar>(
            AdminBaseCarSortStrategyType sortType, 
            SortStrategyDirection sortDirection) where TCar : BaseCar
        {
            switch (sortType)
            {
                case AdminBaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, int>(c => c is NewCar ? 0 : 1);
                case AdminBaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, int>(c => c is NewCar ? 0 : 1);

                case AdminBaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, string>(c => c.Name);
                case AdminBaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, string>(c => c.Name);

                case AdminBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, decimal>(c => c.Price);
                case AdminBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, decimal>(c => c.Price);

                case AdminBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, string>(c => c.Year);
                case AdminBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, string>(c => c.Year);

                case AdminBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, int>(c => c.WarrantyMonthsLeft);
                case AdminBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, int>(c => c.WarrantyMonthsLeft);

                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
