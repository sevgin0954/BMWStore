using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMWStore.Data.Repositories.Generic
{
    public abstract class BaseCarRepository<TCar> : BaseRepository<BaseCar> where TCar : class
    {
        private readonly DbContext dbContext;

        public BaseCarRepository(DbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TCar> GetAllSorted(ICarSortStrategy sortStrategy)
        {
            var cars = dbContext.Set<TCar>().AsQueryable();
            var sortedCars = sortStrategy.Sort((IQueryable<BaseCar>)cars).OfType<TCar>();

            return sortedCars;
        }
    }
}
