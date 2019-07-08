using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<int> CountByRole(string roleId)
        {
            var filteredUsers = this.GetAllAsQueryable()
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.RoleId == roleId));

            return await filteredUsers.CountAsync();
        }

        // TODO: Refactor - method does two things
        public async Task<IEnumerable<User>> GetSortedWithRoleAsync(
            IUserSortStrategy sortStrategy,
            string roleId)
        {
            var filteredUsers = this.GetAllAsQueryable()
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var sortedUsers = sortStrategy
                .Sort(filteredUsers);
            return await sortedUsers.ToArrayAsync();
        }
    }
}