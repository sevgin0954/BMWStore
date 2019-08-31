using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Services.SortStrategies.CarsStrategies;
using BMWStore.Services.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Web.Factories.SortStrategyFactories
{
    public static class UsedCarSortStrategyFactory
    {
        public static ICarSortStrategy<TCar> GetStrategy<TCar>(
            UsedCarSortStrategyType sortType, 
            SortStrategyDirection sortDirection) where TCar : UsedCar
        {
            switch (sortType)
            {
                case UsedCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, decimal>(c => c.Price);
                case UsedCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, decimal>(c => c.Price);

                case UsedCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, string>(c => c.Year);
                case UsedCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, string>(c => c.Year);

                case UsedCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, int>(c => c.WarrantyMonthsLeft);
                case UsedCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, int>(c => c.WarrantyMonthsLeft);

                case UsedCarSortStrategyType.Mileage when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPredicateStrategy<TCar, double>(c => c.Mileage);
                case UsedCarSortStrategyType.Mileage when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPredicateDescStrategy<TCar, double>(c => c.Mileage);

                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
