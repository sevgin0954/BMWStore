using BMWStore.Services.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ITestDriveService
    {
        Task CancelTestDriveAsync(string testDriveId, ClaimsPrincipal user);
        IQueryable<TestDriveServiceModel> GetAll(ClaimsPrincipal user);
        Task<TestDriveServiceModel> GetByIdAsync(string testDriveId, ClaimsPrincipal user);
        Task<string> ScheduleTestDriveAsync(ScheduleTestDriveServiceModel model, ClaimsPrincipal user);
    }
}
