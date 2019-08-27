using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task ChangeTestDriveStatusToPassedAsync(string testDriveId);
        Task DeleteAsync(string testDriveId);
        IQueryable<TestDriveServiceModel> GetAllSorted(
            IQueryable<TestDrive> testDrives,
            ITestDriveSortStrategy sortStrategy,
            int pageNumber);
    }
}
