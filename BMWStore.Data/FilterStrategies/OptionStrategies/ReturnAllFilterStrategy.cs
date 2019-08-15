using System.Linq;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.OptionStrategies
{
    public class ReturnAllFilterStrategy : IOptionFilterStrategy
    {
        public IQueryable<Option> Filter(IQueryable<Option> options)
        {
            return options;
        }
    }
}
