using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    class SortUsersByNamesDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users
                .OrderByDescending(u => u.FirstName)
                .ThenBy(u => u.LastName);

            return sortedUsers;
        }
    }
}
