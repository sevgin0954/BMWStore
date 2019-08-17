using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Tests.Common.SeedTestMethods;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public abstract class BaseAdminTestDrivesServiceTest : BaseTest
    {
        protected IAdminTestDrivesService GetService(ApplicationDbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var statusRepository = new StatusRepository(dbContext);
            this.UpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            this.PassedStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Passed);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var service = new AdminTestDrivesService(testDriveRepository, statusRepository, adminDeleteService);

            return service;
        }

        protected Status UpcomingStatus { get; private set; }

        protected Status PassedStatus { get; private set; }
    }
}
