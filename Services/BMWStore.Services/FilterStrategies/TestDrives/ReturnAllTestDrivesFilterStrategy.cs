using System.Linq;
using BMWStore.Services.FilterStrategies.TestDrives.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.TestDrives
{
    public class ReturnAllTestDrivesFilterStrategy : ITestDriveFilterStrategy
    {
        public IQueryable<TestDrive> Filter(IQueryable<TestDrive> testDrives)
        {
            return testDrives;
        }
    }
}
