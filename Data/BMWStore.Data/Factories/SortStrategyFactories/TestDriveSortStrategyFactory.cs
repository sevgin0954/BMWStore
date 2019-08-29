using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.SortStrategies.TestDriveStrategies;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using System;
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
                    return new SortTestDrivesByPredicateStrategy<DateTime>(td => td.ScheduleDate);
                case AdminTestDrivesSortStrategyType.Price when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPredicateDescStrategy<DateTime>(td => td.ScheduleDate);

                case AdminTestDrivesSortStrategyType.Email when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByPredicateStrategy<string>(td => td.User.Email);
                case AdminTestDrivesSortStrategyType.Email when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPredicateDescStrategy<string>(td => td.User.Email);

                case AdminTestDrivesSortStrategyType.PassedCount when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByPassedCountStrategy();
                case AdminTestDrivesSortStrategyType.PassedCount when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPassedCountDescStrategy();

                case AdminTestDrivesSortStrategyType.Date when sortDirection == SortStrategyDirection.Ascending:
                    return new SortTestDrivesByPredicateStrategy<decimal>(td => td.Car.Price);
                case AdminTestDrivesSortStrategyType.Date when sortDirection == SortStrategyDirection.Descending:
                    return new SortTestDrivesByPredicateDescStrategy<decimal>(td => td.Car.Price);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
