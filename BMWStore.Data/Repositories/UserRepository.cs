using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<User> GetByEmailOrDefault(string email)
        {
            var user = await this.GetAll()
                .Where(u => u.NormalizedEmail == email.ToUpper())
                .FirstOrDefaultAsync();

            return user;
        }

        // TODO: Refactor - method does two things
        public IQueryable<User> GetSortedWithRole(
            IUserSortStrategy sortStrategy,
            string roleId)
        {
            var filteredUsers = this.GetAll()
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var sortedUsers = sortStrategy
                .Sort(filteredUsers);
            return sortedUsers;
        }
    }
}