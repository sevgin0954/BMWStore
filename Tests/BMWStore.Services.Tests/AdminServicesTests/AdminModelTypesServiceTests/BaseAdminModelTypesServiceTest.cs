using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public abstract class BaseAdminModelTypesServiceTest : BaseTest
    {
        public IAdminModelTypesService GetService(ApplicationDbContext dbContext)
        {
            var modelTypeRepository = new ModelTypeRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminModelTypesService(
                modelTypeRepository, 
                adminDeleteService, 
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
