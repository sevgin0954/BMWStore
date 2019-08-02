using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync(ITestDriveSortStrategy sortStrategy);
        Task ChangeTestDriveStatusToPassedAsync(string testDriveId);
        Task DeleteAsync(string testDriveId);
    }
}
