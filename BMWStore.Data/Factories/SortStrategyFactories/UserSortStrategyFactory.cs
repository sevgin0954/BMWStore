using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.UserStrategies;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.SortStrategyFactories
{
    public class UserSortStrategyFactory
    {
        public static IUserSortStrategy GetStrategy(UserSortStrategy sortStrategy)
        {
            switch (sortStrategy)
            {
                case UserSortStrategy.Name:
                    return new SortUsersByNamesStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
