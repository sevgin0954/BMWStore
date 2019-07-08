using System.Threading.Tasks;
using BMWStore.Data.Repositories;
using BMWStore.Data.Repositories.Interfaces;

namespace BMWStore.Data
{
    public class BMWStoreUnitOfWork : IBMWStoreUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public BMWStoreUnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            this.NewCars = new NewCarRepository(this.dbContext);
            this.Users = new UserRepository(this.dbContext);
            this.UsedCars = new UsedCarRepository(this.dbContext);
            this.UsersRoles = new UserRoleRepository(this.dbContext);
            this.Orders = new OrderRepository(this.dbContext);
            this.Roles = new RoleRepository(this.dbContext);
        }

        public NewCarRepository NewCars { get; private set; }

        public UserRepository Users { get; private set; }

        public UsedCarRepository UsedCars { get; private set; }

        public UserRoleRepository UsersRoles { get; private set; }

        public OrderRepository Orders { get; private set; }

        public RoleRepository Roles { get; private set; }

        public int Complete()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }
    }
}