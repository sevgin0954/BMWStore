using System.Linq;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    public class SortUsersByLockoutStatusStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users.OrderBy(u => u.LockoutEnd);

            return sortedUsers;
        }
    }
}
