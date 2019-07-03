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
        }

        public NewCarRepository NewCars { get; private set; }

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
