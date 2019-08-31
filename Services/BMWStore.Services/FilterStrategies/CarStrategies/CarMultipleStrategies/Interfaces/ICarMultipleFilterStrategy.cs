using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces
{
    public interface ICarMultipleFilterStrategy
    {
        IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars);
    }
}
