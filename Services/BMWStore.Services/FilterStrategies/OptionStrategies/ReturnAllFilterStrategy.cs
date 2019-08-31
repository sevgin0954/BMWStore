using System.Linq;
using BMWStore.Services.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.OptionStrategies
{
    public class ReturnAllFilterStrategy : IOptionFilterStrategy
    {
        public IQueryable<Option> Filter(IQueryable<Option> options)
        {
            return options;
        }
    }
}
