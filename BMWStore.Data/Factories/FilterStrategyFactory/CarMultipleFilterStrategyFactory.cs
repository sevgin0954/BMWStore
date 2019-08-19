using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies;
using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Data.Factories.FilterStrategyFactory
{
    public static class CarMultipleFilterStrategyFactory
    {
        public static ICarMultipleFilterStrategy GetStrategy(IEnumerable<string> modelTypes)
        {
            if (modelTypes != null && modelTypes.Count() > 0)
            {
                return new FilterCarsByMultipleModelTypesStrategy(modelTypes.ToArray());
            }
            else
            {
                return new ReturnAllMultipleFilterStrategy();
            }
        }
    }
}
