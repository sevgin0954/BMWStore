using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Entities;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IStatusRepository : IRepository<Status>
    {
        Task<string> GetIdByNameAsync(string name);
    }
}
