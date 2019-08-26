using BMWStore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Data.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        CarRepository AllCars { get; }
        CarOptionRepository CarsOptions { get; }
        EngineRepository Engines { get; }
        FuelTypeRepository FuelTypes { get; }
        ModelTypeRepository ModelTypes { get; }
        NewCarRepository NewCars { get; }
        UserRepository Users { get; }
        UsedCarRepository UsedCars { get; }
        UserRoleRepository UsersRoles { get; }
        OptionRepository Options { get; }
        TestDriveRepository TestDrives { get; }
        RoleRepository Roles { get; }
        SeriesRepository Series { get; }
        PictureRepository Pictures { get; }
        TransmissionRepository Transmissions { get; }
        StatusRepository Statuses { get; }

        int Complete();
        Task<int> CompleteAsync();
        DbQuery<TModel> Query<TModel>() where TModel : class;
    }
}