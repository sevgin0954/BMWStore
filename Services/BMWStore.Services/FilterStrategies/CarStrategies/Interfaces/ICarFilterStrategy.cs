using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.FilterStrategies.CarStrategies.Interfaces
{
    public interface ICarFilterStrategy
    {
        IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars);
    }
}
