using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        NewCarRepository NewCars { get; }
        UserRepository Users { get; }
        UsedCarRepository UsedCars { get; }
        UserOrderedCarRepository UsersOrderedCars { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}