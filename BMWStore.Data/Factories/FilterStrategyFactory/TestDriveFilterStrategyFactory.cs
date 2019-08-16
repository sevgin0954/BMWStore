using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Data.FilterStrategies.TestDrives;
using BMWStore.Data.FilterStrategies.TestDrives.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.FilterStrategyFactory
{
    public static class TestDriveFilterStrategy
    {
        public static ITestDriveFilterStrategy GetStrategy(AdminTestDriveFilterStrategy filterStrategy, string email)
        {
            switch (filterStrategy)
            {
                case AdminTestDriveFilterStrategy.Email:
                    return new FilterTestDrivesByPredicateStrategy(td => td.User.NormalizedEmail == email.ToUpper());
                case AdminTestDriveFilterStrategy.All:
                    return new ReturnAllTestDrivesFilterStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
