using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public abstract class BaseTestDriveServiceTests : BaseTest
    {
        protected ITestDriveService GetService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var statusRepository = new StatusRepository(dbContext);
            var carRepository = new CarRepository(dbContext);
            var servie = new TestDriveService(testDriveRepository, statusRepository, carRepository, userManager);

            return servie;
        }
    }
}
