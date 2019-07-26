using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
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
                    return new SortCarsByPriceStrategy<TCar>();
                case UsedCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy<TCar>();
                case UsedCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case UsedCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                case UsedCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case UsedCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                case UsedCarSortStrategyType.Mileage when sortDirection == SortStrategyDirection.Ascending:
                    return new SortUsedCarsByMileageStrategy<TCar>();
                case UsedCarSortStrategyType.Mileage when sortDirection == SortStrategyDirection.Descending:
                    return new SortUsedCarsByMileageDescStrategy<TCar>();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
