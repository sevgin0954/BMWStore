using System.Linq;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Services.SortStrategies.UserStrategies
{
    public class SortUsersByTestDrivesCountStrategy : IUserSortStrategy
    {
        public IQueryable<User> Sort(IQueryable<User> users)
        {
            var sortedUsers = users
                .Include(u => u.TestDrives)
                .OrderBy(u => u.TestDrives.Count);

            return sortedUsers;
        }
    }
}
