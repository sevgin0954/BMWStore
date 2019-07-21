using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies.Interfaces
{
    public interface ICarSortStrategy
    {
        IQueryable<BaseCar> Sort(IQueryable<BaseCar> cars);
    }
}
