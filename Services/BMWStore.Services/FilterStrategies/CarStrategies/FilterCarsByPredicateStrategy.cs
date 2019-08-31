using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.CarStrategies
{
    public class FilterCarsByPredicateStrategy : ICarFilterStrategy
    {
        private readonly Expression<Func<BaseCar, bool>> predicate;

        public FilterCarsByPredicateStrategy(Expression<Func<BaseCar, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(predicate);

            return filteredCars;
        }
    }
}
