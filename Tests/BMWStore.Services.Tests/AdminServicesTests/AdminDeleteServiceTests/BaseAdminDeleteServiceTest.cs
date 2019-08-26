using BMWStore.Data;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDeleteServiceTests
{
    public abstract class BaseAdminDeleteServiceTest : BaseTest
    {
        public IAdminDeleteService GetService(ApplicationDbContext dbContext)
        {
            var service = new AdminDeleteService(dbContext);

            return service;
        }
    }
}
