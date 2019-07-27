using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ITestDriveService
    {
        Task<TestDriveViewModel> GetTestDriveViewModelAsync(string testDriveId);
        Task ScheduleTestDriveAsync(ScheduleTestDriveBindingModel model, ClaimsPrincipal user);
        Task<IEnumerable<string>> GetAllTestDrivesCarIdsAsync(ClaimsPrincipal user);
    }
}
