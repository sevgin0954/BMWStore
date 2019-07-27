using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbService
    {
        Task SeedAdminAsync(string password, string email);
        Task SeedRolesAsync(params string[] roles);
        Task SeedTestDriveStatuses(params string[] statusNames);
    }
}
