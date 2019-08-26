using System.Linq;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByPriceStrategy : ITestDriveSortStrategy
    {
        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives
                .OrderBy(td => td.Car.Price);

            return sortedTestDrives;
        }
    }
}
