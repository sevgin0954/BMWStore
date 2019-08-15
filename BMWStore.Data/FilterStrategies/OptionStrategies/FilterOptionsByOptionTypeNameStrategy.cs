using System.Linq;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.OptionStrategies
{
    public class FilterOptionsByOptionTypeNameStrategy : IOptionFilterStrategy
    {
        private readonly string optionTypeName;

        public FilterOptionsByOptionTypeNameStrategy(string optionTypeName)
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
