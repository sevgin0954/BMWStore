using BMWStore.Common.Constants;
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
            var filteredModelTypes = GetValidModelTypes(modelTypes);
            if (filteredModelTypes != null)
            {
                return new FilterCarsByMultipleModelTypesStrategy(modelTypes.ToArray());
            }
            else
            {
                return new ReturnAllMultipleFilterStrategy();
            }
        }

        public static IEnumerable<string> GetValidModelTypes(IEnumerable<string> modelTypes)
        {
            if (modelTypes != null)
            {
                var notNullModelTypes = modelTypes
                    .Where(mt => mt != null && mt != WebConstants.AllFilterTypeModelValue)
                    .ToList();
                if (notNullModelTypes.Count > 0)
                {
                    return notNullModelTypes;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
    }
}
