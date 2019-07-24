using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsBySeriesStrategy : ICarFilterStrategy
    {
        private readonly string seriesId;

        public FilterCarsBySeriesStrategy(string seriesId)
        {
            this.seriesId = seriesId;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars
                .Include(c => c.Series)
                .Where(c => c.SeriesId == this.seriesId);

            return filteredCars;
        }
    }
}
