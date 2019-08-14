using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.EngineStrategies
{
    public class SortEnginesDescStrategy<TKey> : IEngineSortStrategy
    {
        private readonly Expression<Func<Engine, TKey>> predicate;

        public SortEnginesDescStrategy(Expression<Func<Engine, TKey>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<Engine> Sort(IQueryable<Engine> engines)
        {
            var sortedEngines = engines.OrderByDescending(predicate);

            return sortedEngines;
        }
    }
}
