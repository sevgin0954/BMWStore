using System.Linq;
using BMWStore.Services.FilterStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.UserStrategies
{
    public class ReturnAllUsersFilterStrategy : IUserFilterStrategy
    {
        public IQueryable<User> Filter(IQueryable<User> users)
        {
            return users;
        }
    }
}
