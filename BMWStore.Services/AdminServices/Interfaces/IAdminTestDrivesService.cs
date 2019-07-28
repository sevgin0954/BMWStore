using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync();
        Task CheckTestDriveStatusAsync(string testDriveId);
        Task DeleteAsync(string testDriveId);
    }
}
