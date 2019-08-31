using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Services.FilterStrategies.CarStrategies;
using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using System.ComponentModel;
using System.Linq;

namespace BMWStore.Web.Factories.FilterStrategyFactory
{
    public class AdminCarFilterStrategyFactory
    {
        public static ICarFilterStrategy GetStrategy(AdminBaseCarFilterStrategy filterStrategy, string name)
        {
            switch (filterStrategy)
            {
                case AdminBaseCarFilterStrategy.Engine:
                    return new FilterCarsByPredicateStrategy(c => c.Engine.Name == name);
                case AdminBaseCarFilterStrategy.Option:
                    return new FilterCarsByPredicateStrategy(c => c.Options.Any(o => o.Option.Name == name));
                case AdminBaseCarFilterStrategy.FuelType:
                    return new FilterCarsByPredicateStrategy(c => c.FuelType.Name == name);
                case AdminBaseCarFilterStrategy.ModelType:
                    return new FilterCarsByPredicateStrategy(c => c.ModelType.Name == name);
                case AdminBaseCarFilterStrategy.Series:
                    return new FilterCarsByPredicateStrategy(c => c.Series.Name == name);
                case AdminBaseCarFilterStrategy.All:
                    return new ReturnAllFilterStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
