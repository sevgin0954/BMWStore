using System.Linq;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.UserStrategies
{
    public class SortUsersByEmailStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users.OrderBy(u => u.NormalizedEmail);

            return sortedUsers;
        }
    }
}
