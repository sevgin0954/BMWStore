using BMWStore.Common.Enums;
using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<AdminTestDrivesViewModel> GetTestDriveViewModelAsync(
            AdminTestDrivesSortStrategyType sortType,
            SortStrategyDirection sortDirection,
            int pageNumber);
        Task ChangeTestDriveStatusToPassedAsync(string testDriveId);
    }
}
