using System.Linq;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.UserStrategies
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
