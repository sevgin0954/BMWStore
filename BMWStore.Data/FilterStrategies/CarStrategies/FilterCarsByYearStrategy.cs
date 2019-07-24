using System.Linq;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByYearStrategy : ICarFilterStrategy
    {
        private readonly int startYear;

        public FilterCarsByYearStrategy(int startYear)
        {
            DataValidator.ValidateYearString(startYear.ToString());
            this.startYear = startYear;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c => int.Parse(c.Year) >= this.startYear);

            return filteredCars;
        }
    }
}
