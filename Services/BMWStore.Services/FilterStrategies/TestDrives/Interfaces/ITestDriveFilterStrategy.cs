using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.FilterStrategies.TestDrives.Interfaces
{
    public interface ITestDriveFilterStrategy
    {
        IQueryable<TestDrive> Filter(IQueryable<TestDrive> testDrives);
    }
}
