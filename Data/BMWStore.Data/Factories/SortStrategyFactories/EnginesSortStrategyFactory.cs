using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.SortStrategies.EngineStrategies;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class EnginesSortStrategyFactory
    {
        public static IEngineSortStrategy GetStrategy(
            EngineSortStrategy sortStrategyType,
            SortStrategyDirection sortDirection)
        {
            switch (sortStrategyType)
            {
                case EngineSortStrategy.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortEnginesStrategy<string>(e => e.Name);
                case EngineSortStrategy.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortEnginesDescStrategy<string>(e => e.Name);
                case EngineSortStrategy.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortEnginesStrategy<decimal>(e => e.Price);
                case EngineSortStrategy.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortEnginesDescStrategy<decimal>(e => e.Price);
                case EngineSortStrategy.Weight when sortDirection == SortStrategyDirection.Ascending:
                    return new SortEnginesStrategy<int>(e => e.Weight_Kg);
                case EngineSortStrategy.Weight when sortDirection == SortStrategyDirection.Descending:
                    return new SortEnginesDescStrategy<int>(e => e.Weight_Kg);
                case EngineSortStrategy.Cars when sortDirection == SortStrategyDirection.Ascending:
                    return new SortEnginesStrategy<int>(e => e.Cars.Count);
                case EngineSortStrategy.Cars when sortDirection == SortStrategyDirection.Descending:
                    return new SortEnginesDescStrategy<int>(e => e.Cars.Count);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
