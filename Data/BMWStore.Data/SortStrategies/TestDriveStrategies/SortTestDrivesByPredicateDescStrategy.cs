using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByPredicateDescStrategy<TKey> : ITestDriveSortStrategy
    {
        private readonly Expression<Func<TestDrive, TKey>> predicate;

        public SortTestDrivesByPredicateDescStrategy(Expression<Func<TestDrive, TKey>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives.OrderByDescending(this.predicate);

            return sortedTestDrives;
        }
    }
}
