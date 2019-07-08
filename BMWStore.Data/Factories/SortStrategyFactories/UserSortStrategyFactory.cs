using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.UserStrategies;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class UserSortStrategyFactory
    {
        public static IUserSortStrategy GetStrategy(UserSortStrategyType sortStrategy, SortStrategyDirection sortDirection)
        {
            switch (sortStrategy)
            {
                case UserSortStrategyType.Name when sortDirection == SortStrategyDirection.Ascending:
                    return new SortUsersByNamesStrategy();
                case UserSortStrategyType.Name when sortDirection == SortStrategyDirection.Descending:
                    return new SortUsersByNamesDescStrategy();
                case UserSortStrategyType.Orders when sortDirection == SortStrategyDirection.Ascending:
                    return new SortUsersByOrdersCountStrategy();
                case UserSortStrategyType.Orders when sortDirection == SortStrategyDirection.Descending:
                    return new SortUsersByOrdersCountDescStrategy();
                case UserSortStrategyType.LockoutStatus when sortDirection == SortStrategyDirection.Ascending:
                    return new SortUsersByLockoutStatusStrategy();
                case UserSortStrategyType.LockoutStatus when sortDirection == SortStrategyDirection.Descending:
                    return new SortUsersByLockoutStatusDescStrategy();
                case UserSortStrategyType.Email when sortDirection == SortStrategyDirection.Ascending:
                    return new SortUsersByEmailStrategy();
                case UserSortStrategyType.Email when sortDirection == SortStrategyDirection.Descending:
                    return new SortUsersByEmailDescStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
