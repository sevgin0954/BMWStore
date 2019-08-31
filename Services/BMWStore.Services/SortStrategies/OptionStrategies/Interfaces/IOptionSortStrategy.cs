using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.SortStrategies.OptionStrategies.Interfaces
{
    public interface IOptionSortStrategy
    {
        IQueryable<Option> Sort(IQueryable<Option> options);
    }
}
