using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class NewCarSortStrategyFactory
    {
        public static ICarSortStrategy<TCar> GetStrategy<TCar>(
            BaseCarSortStrategyType sortType,
            SortStrategyDirection sortDirection) where TCar : NewCar
        {
            switch (sortType)
            {
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy<TCar>();
                case BaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy<TCar>();
                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case BaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case BaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
