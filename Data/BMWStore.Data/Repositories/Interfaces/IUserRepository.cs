using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailOrDefault(string email);
        IQueryable<User> GetSortedWithRole(
            IUserSortStrategy sortStrategy,
            string roleId);
        Task<User> GetByIdWithRolesAsync(string userId);
    }
}
