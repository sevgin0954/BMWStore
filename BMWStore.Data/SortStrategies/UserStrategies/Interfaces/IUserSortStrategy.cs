using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.UserStrategies.Interfaces
{
    public interface IUserSortStrategy
    {
        IQueryable<User> Sort(IQueryable<User> users);
    }
}
