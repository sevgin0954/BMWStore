using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Services.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.CarsStrategies
{
    public class SortCarsByPredicateDescStrategy<TCar, TKey> : ICarSortStrategy<TCar> where TCar : BaseCar
    {
        private readonly Expression<Func<TCar, TKey>> predicate;

        public SortCarsByPredicateDescStrategy(Expression<Func<TCar, TKey>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<TCar> Sort(IQueryable<TCar> cars)
        {
            var sortedCars = cars.OrderByDescending(this.predicate);

            return sortedCars;
        }
    }
}
