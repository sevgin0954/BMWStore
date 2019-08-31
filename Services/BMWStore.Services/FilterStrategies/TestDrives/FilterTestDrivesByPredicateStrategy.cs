using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Services.FilterStrategies.TestDrives.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.TestDrives
{
    public class FilterTestDrivesByPredicateStrategy : ITestDriveFilterStrategy
    {
        private readonly Expression<Func<TestDrive, bool>> predicate;

        public FilterTestDrivesByPredicateStrategy(Expression<Func<TestDrive, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<TestDrive> Filter(IQueryable<TestDrive> testDrives)
        {
            var filteredTestDrives = testDrives.Where(predicate);

            return filteredTestDrives;
        }
    }
}
