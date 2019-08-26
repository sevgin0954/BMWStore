using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMWStore.Data.SortStrategies.UserStrategies
{
    class SortUsersByTestDrivesCountDescStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users
                .Include(u => u.TestDrives)
                .OrderByDescending(u => u.TestDrives.Count);

            return sortedUsers;
        }
    }
}
