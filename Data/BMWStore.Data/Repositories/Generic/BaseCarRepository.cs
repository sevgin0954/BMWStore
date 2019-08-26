using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMWStore.Data.Repositories.Generic
{
    public abstract class BaseCarRepository<TCar> : BaseRepository<TCar>, IBaseCarRepository<TCar> where TCar : BaseCar
    {
        private readonly DbContext dbContext;

        public BaseCarRepository(DbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TCar> GetAllSorted(ICarSortStrategy<TCar> sortStrategy)
        {
            var cars = dbContext.Set<TCar>().AsQueryable();
            var sortedCars = sortStrategy.Sort(cars).OfType<TCar>();

            return sortedCars;
        }

        public virtual IQueryable<TCar> GetFiltered(params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.dbContext.Set<TCar>().AsQueryable();

            foreach (var strategy in filterStrategies)
            {
                filteredCars = strategy.Filter(filteredCars).OfType<TCar>();
            }

            return filteredCars;
        }
    }
}
