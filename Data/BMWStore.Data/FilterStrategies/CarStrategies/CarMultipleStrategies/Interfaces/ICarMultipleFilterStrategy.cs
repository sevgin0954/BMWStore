using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces
{
    public interface ICarMultipleFilterStrategy
    {
        IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars);
    }
}
