using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.Repositories.Generic.Interfaces
{
    public interface IBaseCarRepository<TCar> where TCar : BaseCar
    {
        IQueryable<TCar> GetAllSorted(ICarSortStrategy<TCar> sortStrategy);
        IQueryable<TCar> GetFiltered(params ICarFilterStrategy[] filterStrategies);
    }
}
