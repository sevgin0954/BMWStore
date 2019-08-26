using BMWStore.Data.Repositories.Generic.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
        Task<string> GetIdByNameAsync(string name);
    }
}
