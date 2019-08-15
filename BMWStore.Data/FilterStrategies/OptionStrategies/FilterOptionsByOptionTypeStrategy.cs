using System.Linq;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.OptionStrategies
{
    public class FilterOptionsByOptionTypeStrategy : IOptionFilterStrategy
    {
        private readonly string optionTypeName;

        public FilterOptionsByOptionTypeStrategy(string optionTypeName)
        {
            this.optionTypeName = optionTypeName;
        }

        public IQueryable<Option> Filter(IQueryable<Option> options)
        {
            var filteredOptions = options.Where(o => o.OptionType.Name == optionTypeName);

            return filteredOptions;
        }
    }
}
