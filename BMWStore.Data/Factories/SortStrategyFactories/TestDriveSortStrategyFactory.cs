using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.TestDriveStrategies;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public static class TestDriveSortStrategyFactory
    {
        public static ITestDriveSortStrategy GetStrategy(
            AdminTestDrivesSortStrategyType sortStrategyType, 
            SortStrategyDirection sortDirection)
        {
            switch (sortStrategyType)
            {
                case AdminTestDrivesSortStrategyType.Status when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByStatusStrategy();
                case AdminTestDrivesSortStrategyType.Status when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByStatusDescStrategy();
                case AdminTestDrivesSortStrategyType.Price when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByPriceStrategy();
                case AdminTestDrivesSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPriceDescStrategy();
                case AdminTestDrivesSortStrategyType.Email when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByEmailStrategy();
                case AdminTestDrivesSortStrategyType.Email when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByEmailDescStrategy();
                case AdminTestDrivesSortStrategyType.PassedCount when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByPassedCountStrategy();
                case AdminTestDrivesSortStrategyType.PassedCount when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPassedCountDescStrategy();
                case AdminTestDrivesSortStrategyType.Date when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByDateStrategy();
                case AdminTestDrivesSortStrategyType.Date when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByDateDescStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
