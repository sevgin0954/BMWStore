using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public abstract class BaseAdminModelTypesServiceTest : BaseTest
    {
        public IAdminModelTypesService GetService(ApplicationDbContext dbContext)
        {
            var modelTypeRepository = new ModelTypeRepository(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var adminEditService = new AdminEditService(dbContext);
            var adminCreateService = new AdminCreateService(dbContext);
            var service = new AdminModelTypesService(
                modelTypeRepository, 
                adminDeleteService, 
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
