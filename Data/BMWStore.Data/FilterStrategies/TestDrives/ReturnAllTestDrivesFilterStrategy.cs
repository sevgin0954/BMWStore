using System.Linq;
using BMWStore.Data.FilterStrategies.TestDrives.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.TestDrives
{
    public class ReturnAllTestDrivesFilterStrategy : ITestDriveFilterStrategy
    {
        public IQueryable<TestDrive> Filter(IQueryable<TestDrive> testDrives)
        {
            return testDrives;
        }
    }
}
