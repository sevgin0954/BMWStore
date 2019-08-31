using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.SortStrategies.UserStrategies
{
    public class SortUsersByLockoutStatusDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users.OrderByDescending(u => u.LockoutEnd);

            return sortedUsers;
        }
    }
}
