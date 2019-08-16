using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.FilterStrategies.TestDrives.Interfaces;
using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<AdminTestDrivesViewModel> GetTestDrivesViewModelAsync(
            ITestDriveFilterStrategy filterStrategy,
            AdminTestDrivesSortStrategyType sortType,
            SortStrategyDirection sortDirection,
            int pageNumber);
        Task ChangeTestDriveStatusToPassedAsync(string testDriveId);
        Task DeleteAsync(string testDriveId);
    }
}
