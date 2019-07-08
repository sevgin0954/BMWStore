using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    class SortUsersByLockoutStatusDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users.OrderByDescending(u => u.LockoutEnd);

            return sortedUsers;
        }
    }
}
