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
                    return new FilterCarsByEngineIdStrategy(id);
                case AdminBaseCarFilterStrategy.Option:
                    return new FilterCarsByOptionIdStrategy(id);
                case AdminBaseCarFilterStrategy.FuelType:
                    return new FilterCarsByFuelTypeIdStrategy(id);
                case AdminBaseCarFilterStrategy.ModelType:
                    return new FilterCarsByModelTypeIdStrategy(id);
                case AdminBaseCarFilterStrategy.All:
                    return new ReturnAllFilterStrategy();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
