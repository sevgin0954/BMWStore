using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByPassedCountDescStrategy : ITestDriveSortStrategy
    {
        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives
                .OrderByDescending(td => td.User.TestDrives
                    .Count(utd => utd.Status.Name == TestDriveStatus.Passed.ToString()));

            return sortedTestDrives;
        }
    }
}
