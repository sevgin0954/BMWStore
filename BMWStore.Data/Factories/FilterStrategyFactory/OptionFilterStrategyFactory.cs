using BMWStore.Common.Enums;
using BMWStore.Data.FilterStrategies.OptionStrategies;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.FilterStrategyFactory
{
    public static class OptionFilterStrategyFactory
    {
        public static IOptionFilterStrategy GetStrategy(AdminOptionFilterStrategy optionFilterStrategy, string name)
        {
            switch (optionFilterStrategy)
            {
                case AdminOptionFilterStrategy.All:
                    return new ReturnAllFilterStrategy();
                case AdminOptionFilterStrategy.OptionType:
                    return new FilterOptionsByOptionTypeStrategy(name);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
