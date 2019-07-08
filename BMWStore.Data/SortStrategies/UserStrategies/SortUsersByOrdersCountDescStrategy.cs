using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    class SortUsersByOrdersCountDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users
                .Include(u => u.Orders)
                .OrderByDescending(u => u.Orders.Count);

            return sortedUsers;
        }
    }
}
