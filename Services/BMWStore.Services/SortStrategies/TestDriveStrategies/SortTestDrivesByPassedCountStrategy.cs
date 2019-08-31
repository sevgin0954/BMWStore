using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Services.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByPassedCountStrategy : ITestDriveSortStrategy
    {
        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives
                .OrderBy(td => td.User.TestDrives
                    .Count(utd => utd.Status.Name == TestDriveStatus.Passed.ToString()));

            return sortedTestDrives;
        }
    }
}
