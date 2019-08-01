using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public abstract class BaseAdminEnginesServiceTest
    {
        public IAdminEnginesService GetService(ApplicationDbContext dbContext)
        {
            var engineRepository = new EngineRepository(dbContext);
            var selectListItemsService = new SelectListItemsService();
            var service = new AdminEnginesService(engineRepository, selectListItemsService);

            return service;
        }
    }
}
