using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.FilterStrategies.TestDrives.Interfaces
{
    public interface ITestDriveFilterStrategy
    {
        IQueryable<TestDrive> Filter(IQueryable<TestDrive> testDrives);
    }
}
