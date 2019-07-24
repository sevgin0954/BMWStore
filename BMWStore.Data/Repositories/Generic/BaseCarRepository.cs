using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMWStore.Data.Repositories.Generic
{
    public abstract class BaseCarRepository<TCar> : BaseRepository<TCar> where TCar : class
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

        public IQueryable<TCar> CountByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var result = dbContext.Set<TCar>().AsQueryable()
                .FromSql(
                "SELECT COUNT(*) AS [Count] FROM (" +
                "	SELECT CASE " +
                $"		WHEN bc.Price >= @minPrice AND bc.Price <= @maxPrice" +
                $"		END AS Range" +
                "	FROM Planes AS p" +
                ") AS ta" +
                "GROUP BY Range", minPrice, maxPrice);

            return result;
        }

        public virtual IQueryable<TCar> GetFiltered(params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.dbContext.Set<TCar>().AsQueryable();

            foreach (var strategy in filterStrategies)
            {
                filteredCars = strategy.Filter((IQueryable<BaseCar>)filteredCars).OfType<TCar>();
            }

            return filteredCars;
        }
    }
}
