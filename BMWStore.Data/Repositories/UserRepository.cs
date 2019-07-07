using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<User>> GetSortedByAsync(IUserSortStrategy sortStrategy)
        {
            var sortedUsers = sortStrategy.Sort(this.GetAllAsQueryable());
            return await sortedUsers.ToArrayAsync();
        }
    }
}