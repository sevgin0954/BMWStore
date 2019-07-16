using BMWStore.Data.Repositories;
using System.Threading.Tasks;

namespace BMWStore.Data.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        CarRepository AllCars { get; }
        EngineRepository Engines { get; }
        FuelTypeRepository FuelTypes { get; }
        ModelTypeRepository ModelTypes { get; }
        NewCarRepository NewCars { get; }
        UserRepository Users { get; }
        UsedCarRepository UsedCars { get; }
        UserRoleRepository UsersRoles { get; }
        OrderRepository Orders { get; }
        RoleRepository Roles { get; }
        SeriesRepository Series { get; }
        TransmissionRepository Transmissions { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}