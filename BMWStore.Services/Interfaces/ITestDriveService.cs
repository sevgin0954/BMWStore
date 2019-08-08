using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ITestDriveService
    {
        Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync(ClaimsPrincipal user);
        Task<TestDriveViewModel> GetTestDriveAsync(string testDriveId, ClaimsPrincipal user);
        Task<string> ScheduleTestDriveAsync(ScheduleTestDriveBindingModel model, ClaimsPrincipal user);
        Task CancelTestDriveAsync(string testDriveId, ClaimsPrincipal user);
    }
}
