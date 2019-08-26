using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public abstract class BaseAdminEnginesServiceTest : BaseTest
    {
        protected IAdminEnginesService GetService(ApplicationDbContext dbContext)
        {
            var engineRepository = new EngineRepository(dbContext);
            var readService = new ReadService(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var adminEditService = new AdminEditService(dbContext);
            var service = new AdminEnginesService(
                engineRepository,
                adminDeleteService,
                adminEditService,
                readService);

            return service;
        }
    }
}
