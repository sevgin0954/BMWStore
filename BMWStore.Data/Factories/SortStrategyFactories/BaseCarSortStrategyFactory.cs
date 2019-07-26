using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.CarsStrategies;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class BaseCarSortStrategyFactory
    {
        public static ICarSortStrategy<TCar> GetStrategy<TCar>(
            AdminBaseCarSortStrategyType sortType, 
            SortStrategyDirection sortDirection) where TCar : BaseCar
        {
            switch (sortType)
            {
                case AdminBaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByConditionStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Condition when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByConditionDescStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByNameStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByNameDescStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByPriceStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByPriceDescStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Year when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Ascending:
                    return new SortCarsByYearStrategy<TCar>();
                case AdminBaseCarSortStrategyType.Warranty when sortDirection == SortStrategyDirection.Descending:
                    return new SortCarsByYearDescStrategy<TCar>();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
