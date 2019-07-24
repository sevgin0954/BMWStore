using System.Threading.Tasks;
using BMWStore.Data.Repositories;
using BMWStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data
{
    public class BMWStoreUnitOfWork : IBMWStoreUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public BMWStoreUnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            this.AllCars = new CarRepository(this.dbContext);
            this.CarsOptions = new CarOptionRepository(this.dbContext);
            this.Engines = new EngineRepository(this.dbContext);
            this.FuelTypes = new FuelTypeRepository(this.dbContext);
            this.ModelTypes = new ModelTypeRepository(this.dbContext);
            this.NewCars = new NewCarRepository(this.dbContext);
            this.Users = new UserRepository(this.dbContext);
            this.UsedCars = new UsedCarRepository(this.dbContext);
            this.UsersRoles = new UserRoleRepository(this.dbContext);
            this.Options = new OptionRepository(this.dbContext);
            this.Orders = new OrderRepository(this.dbContext);
            this.Roles = new RoleRepository(this.dbContext);
            this.Series = new SeriesRepository(this.dbContext);
            this.Pictures = new PictureRepository(this.dbContext);
            this.Transmissions = new TransmissionRepository(this.dbContext);
        }

        public CarRepository AllCars { get; private set; }

        public CarOptionRepository CarsOptions { get; private set; }
        public EngineRepository Engines { get; private set; }

        public FuelTypeRepository FuelTypes { get; private set; }

        public ModelTypeRepository ModelTypes { get; private set; }

        public NewCarRepository NewCars { get; private set; }

        public UserRepository Users { get; private set; }

        public UsedCarRepository UsedCars { get; private set; }

        public UserRoleRepository UsersRoles { get; private set; }

        public OptionRepository Options { get; private set; }

        public OrderRepository Orders { get; private set; }

        public RoleRepository Roles { get; private set; }

        public SeriesRepository Series { get; private set; }

        public PictureRepository Pictures { get; private set; }

        public TransmissionRepository Transmissions { get; private set; }

        public int Complete()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public DbQuery<TModel> Query<TModel>() where TModel : class
        {
            return this.dbContext.Query<TModel>();
        }
    }
}