using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public abstract class BaseAdminTransmissionsServiceTests : BaseTest
    {
        public IAdminTransmissionsService GetService(ApplicationDbContext dbContext)
        {
            var transmissionRepository = new TransmissionRepository(dbContext);
            var service = new AdminTransmissionsService(transmissionRepository);

            return service;
        }
    }
}
