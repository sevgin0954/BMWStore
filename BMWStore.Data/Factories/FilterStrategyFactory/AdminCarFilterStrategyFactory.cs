using BMWStore.Common.Enums;
using BMWStore.Data.FilterStrategies.CarStrategies;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using System.ComponentModel;

namespace BMWStore.Data.Factories.FilterStrategyFactory
{
    public class AdminCarFilterStrategyFactory
    {
        public static ICarFilterStrategy GetStrategy(AdminBaseCarFilterStrategy filterStrategy, string id)
        {
            switch (filterStrategy)
            {
                case AdminBaseCarFilterStrategy.Engine:
                    return new FilterCarsByEngineStrategy(id);
                case AdminBaseCarFilterStrategy.Option:
                    return new FilterCarsByOptionStrategy(id);
                case AdminBaseCarFilterStrategy.FuelType:
                    return new FilterCarsByFuelTypeStrategy(id);
                case AdminBaseCarFilterStrategy.All:
                    return new ReturnAllFilterStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
