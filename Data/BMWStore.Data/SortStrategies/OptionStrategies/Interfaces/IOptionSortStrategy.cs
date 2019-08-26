using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.OptionStrategies.Interfaces
{
    public interface IOptionSortStrategy
    {
        IQueryable<Option> Sort(IQueryable<Option> options);
    }
}
