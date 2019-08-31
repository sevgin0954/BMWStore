using BMWStore.Common.Constants;
using BMWStore.Services.FilterStrategies.CarStrategies;
using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Web.Factories.FilterStrategyFactory
{
    public static class CarSearchFilterStrategyFactory
    {
        public static ICollection<ICarFilterStrategy> GetStrategies(string[] keyWords)
        {
            var strategies = new List<ICarFilterStrategy>();

            if (keyWords != null && keyWords.Length > 0)
            {
                foreach (var keyWord in keyWords)
                {
                    if (string.IsNullOrEmpty(keyWord) == false)
                    {
                        var strategy = new FilterCarsByKeyWordStrategy(keyWord);
                        strategies.Add(strategy);
                    }
                }
            }

            return strategies;
        }
    }
}
