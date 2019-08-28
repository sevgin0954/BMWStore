using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public abstract class BaseAdminTransmissionsServiceTests : BaseTest
    {
        public IAdminTransmissionsService GetService(ApplicationDbContext dbContext)
        {
            var transmissionRepository = new TransmissionRepository(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminTransmissionsService(
                transmissionRepository,
                adminEditService,
                adminDeleteService,
                adminCreateService);

            return service;
        }
    }
}
