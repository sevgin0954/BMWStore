using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbRolesService
    {
        Task SeedRolesAsync(params string[] roleNames);
    }
}
