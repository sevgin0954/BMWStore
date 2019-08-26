using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.SeedDbStatusesServiceTests
{
    public abstract class BaseSeedDbStatusesServiceTest : BaseTest
    {
        public ISeedDbStatusesService GetService(ApplicationDbContext dbContext)
        {
            var statusRepository = new StatusRepository(dbContext);
            var service = new SeedDbStatusesService(statusRepository);

            return service;
        }
    }
}
