using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public abstract class BaseAdminModelTypesServiceTest : BaseTest
    {
        public IAdminModelTypesService GetService(ApplicationDbContext dbContext)
        {
            var modelTypeRepository = new ModelTypeRepository(dbContext);
            var service = new AdminModelTypesService(modelTypeRepository);

            return service;
        }
    }
}
