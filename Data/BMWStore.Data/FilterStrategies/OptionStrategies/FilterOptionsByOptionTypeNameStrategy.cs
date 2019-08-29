using System;
using System.Linq;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.OptionStrategies
{
    public class FilterOptionsByOptionTypeNameStrategy : IOptionFilterStrategy
    {
        private readonly string optionTypeName;

        public FilterOptionsByOptionTypeNameStrategy(string optionTypeName)
        {
            var exception = new ArgumentException(ErrorConstants.CantBeNullOrEmptyParameter, nameof(optionTypeName));
            DataValidator.ValidateNotNullOrEmpty(optionTypeName, exception);

            this.optionTypeName = optionTypeName;
        }

        public IQueryable<Option> Filter(IQueryable<Option> options)
        {
            var filteredOptions = options.Where(o => o.OptionType.Name == optionTypeName);

            return filteredOptions;
        }
    }
}
