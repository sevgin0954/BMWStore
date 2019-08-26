using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.FilterStrategies.UserStrategies.Interfaces
{
    public interface IUserFilterStrategy
    {
        IQueryable<User> Filter(IQueryable<User> users);
    }
}
