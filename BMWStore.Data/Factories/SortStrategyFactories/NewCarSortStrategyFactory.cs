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
            NewCarSortStrategyType sortType, 
            SortStrategyDirection sortDirection) where TCar : NewCar
        {
            switch (sortType)
            {
                case NewCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy<TCar>();
                case NewCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy<TCar>();
                case NewCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case NewCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                case NewCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case NewCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
