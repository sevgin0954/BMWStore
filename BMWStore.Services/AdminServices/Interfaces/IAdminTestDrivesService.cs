using BMWStore.Common.Enums;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTestDrivesService
    {
        Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync();
        Task ChangeTestDriveStatusAsync(
            TestDriveStatus newStatus,
            string carId,
            ClaimsPrincipal user);
    }
}
