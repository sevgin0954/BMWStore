using System.Linq;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    public class SortUsersByEmailDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users.OrderByDescending(u => u.NormalizedEmail);

            return sortedUsers;
        }
    }
}
