using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces
{
    public interface ITestDriveSortStrategy
    {
        IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives);
    }
}
