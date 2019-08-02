using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public abstract class BaseAdminTestDrivesServiceTest
    {
        protected IAdminTestDrivesService GetService(ApplicationDbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var statusRepository = new StatusRepository(dbContext);
            var service = new AdminTestDrivesService(testDriveRepository, statusRepository);

            return service;
        }

        protected TestDrive SeedTestDriveWithStatuses(ApplicationDbContext dbContext, TestDriveStatus status = TestDriveStatus.Upcoming)
        {
            var dbStatus = this.CreateStatus(dbContext, status);
            this.CreateStatus(dbContext, TestDriveStatus.Upcoming);
            this.CreateStatus(dbContext, TestDriveStatus.Passed);
            var dbTestDrive = new TestDrive()
            {
                Status = dbStatus,
                User = new User(),
                Car = this.CreateCarWithPicture()
            };
            dbContext.TestDrives.Add(dbTestDrive);

            dbContext.SaveChanges();

            return dbTestDrive;
        }

        private BaseCar CreateCarWithPicture()
        {
            var dbCar = new NewCar();
            var dbPicture = new Picture();
            dbCar.Pictures.Add(dbPicture);

            return dbCar;
        }

        protected Status CreateStatus(ApplicationDbContext dbContext, TestDriveStatus status)
        {
            var dbStatus = new Status()
            {
                Name = status.ToString()
            };
            dbContext.Statuses.Add(dbStatus);

            return dbStatus;
        }
    }
}
