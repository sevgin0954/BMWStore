using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Entities;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface ICarOptionRepository : IRepository<CarOption>
    {
        Task RemoveAllWithCarIdAsync(string carId);
    }
}
