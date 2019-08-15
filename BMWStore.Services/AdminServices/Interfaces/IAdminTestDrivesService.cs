using BMWStore.Common.Enums;
using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<AdminTestDrivesViewModel> GetTestDrivesViewModelAsync(
            AdminTestDrivesSortStrategyType sortType,
            SortStrategyDirection sortDirection,
            int pageNumber);
        Task ChangeTestDriveStatusToPassedAsync(string testDriveId);
        Task DeleteAsync(string testDriveId);
    }
}
