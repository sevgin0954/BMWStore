using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbStatusesService
    {
        Task SeedTestDriveStatuses(params string[] statusNames);
    }
}
