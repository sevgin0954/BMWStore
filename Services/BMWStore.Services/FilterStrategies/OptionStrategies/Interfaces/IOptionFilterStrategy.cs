using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.FilterStrategies.OptionStrategies.Interfaces
{
    public interface IOptionFilterStrategy
    {
        IQueryable<Option> Filter(IQueryable<Option> options);
    }
}
