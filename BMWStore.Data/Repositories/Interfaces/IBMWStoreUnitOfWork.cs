using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        int Complete();
        Task<int> CompleteAsync();
    }
}
