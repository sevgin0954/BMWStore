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
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminEnginesService(
                engineRepository,
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
