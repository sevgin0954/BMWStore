using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.FilterStrategies.CarStrategies.Interfaces
{
    public interface ICarFilterStrategy
    {
        IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars);
    }
}
