using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.SortStrategies.TestDriveStrategies.Interfaces
{
    public interface ITestDriveSortStrategy
    {
        IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives);
    }
}
