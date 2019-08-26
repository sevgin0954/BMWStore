using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.EngineStrategies.Interfaces
{
    public interface IEngineSortStrategy
    {
        IQueryable<Engine> Sort(IQueryable<Engine> engines);
    }
}
