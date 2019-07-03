using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        NewCarRepository NewCars { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
