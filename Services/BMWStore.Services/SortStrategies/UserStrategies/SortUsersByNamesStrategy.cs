using System.Linq;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.SortStrategies.UserStrategies
{
    public class SortUsersByNamesStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName);

            return sortedUsers;
        }
    }
}
