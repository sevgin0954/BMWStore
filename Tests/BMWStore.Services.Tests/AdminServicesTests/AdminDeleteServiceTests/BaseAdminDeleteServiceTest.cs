using BMWStore.Data;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDeleteServiceTests
{
    public abstract class BaseAdminDeleteServiceTest : BaseTest
    {
        public IAdminCommonDeleteService GetService(ApplicationDbContext dbContext)
        {
            var service = new AdminCommonDeleteService(dbContext);

            return service;
        }
    }
}
