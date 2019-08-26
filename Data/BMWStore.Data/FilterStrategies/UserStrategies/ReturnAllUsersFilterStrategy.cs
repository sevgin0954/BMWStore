using System.Linq;
using BMWStore.Data.FilterStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.UserStrategies
{
    public class ReturnAllUsersFilterStrategy : IUserFilterStrategy
    {
        public IQueryable<User> Filter(IQueryable<User> users)
        {
            return users;
        }
    }
}
