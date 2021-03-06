﻿using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Services.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByPredicateStrategy<TKey> : ITestDriveSortStrategy
    {
        private readonly Expression<Func<TestDrive, TKey>> predicate;

        public SortTestDrivesByPredicateStrategy(Expression<Func<TestDrive, TKey>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrive = testDrives.OrderBy(this.predicate);

            return sortedTestDrive;
        }
    }
}
