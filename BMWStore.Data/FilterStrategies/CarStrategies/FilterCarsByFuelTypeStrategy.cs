using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    class FilterCarsByFuelTypeStrategy : ICarFilterStrategy
    {
        private readonly string fuelTypeId;

        public FilterCarsByFuelTypeStrategy(string fuelTypeId)
        {
            this.fuelTypeId = fuelTypeId;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c => c.FuelTypeId == this.fuelTypeId);

            return filteredCars;
        }
    }
}
