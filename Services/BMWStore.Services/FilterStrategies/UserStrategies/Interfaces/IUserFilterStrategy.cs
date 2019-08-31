using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Services.FilterStrategies.UserStrategies.Interfaces
{
    public interface IUserFilterStrategy
    {
        IQueryable<User> Filter(IQueryable<User> users);
    }
}
