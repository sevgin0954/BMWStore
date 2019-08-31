using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Entities;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailOrDefault(string email);
        Task<User> GetByIdWithRolesAsync(string userId);
    }
}
