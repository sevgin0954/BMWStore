﻿using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.EngineStrategies
{
    public class SortEnginesStrategy<TKey> : IEngineSortStrategy
    {
        private readonly Expression<Func<Engine, TKey>> predicate;

        public SortEnginesStrategy(Expression<Func<Engine, TKey>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<Engine> Sort(IQueryable<Engine> engines)
        {
            var sortedEngines = engines.OrderBy(predicate);

            return sortedEngines;
        }
    }
}
