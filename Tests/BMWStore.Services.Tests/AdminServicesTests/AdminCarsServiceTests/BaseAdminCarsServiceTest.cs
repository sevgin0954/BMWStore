using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockMethods;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest : BaseTest
    {
        protected IAdminCarsService GetService(ApplicationDbContext dbContext)
        {
            var carRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var pictureRepository = new PictureRepository(dbContext);
            var service = new AdminCarsService(
                carRepository, 
                carOptionsRepository,
                pictureRepository,
                adminDeleteService);

            return service;
        }

        private ICarsService GetCarService(ApplicationDbContext dbContext, ICarRepository carRepository)
        {
            var userManager = CommonMockMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockMethods.GetMockedSignInManager(userManager).Object;
            var readService = new ReadService(dbContext);
            var carsService = new CarsService(signInManager, carRepository, readService);

            return carsService;
        }
    }
}
