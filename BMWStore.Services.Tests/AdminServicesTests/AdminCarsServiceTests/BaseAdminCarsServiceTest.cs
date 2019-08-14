using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common.MockTestMethods;
using Moq;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest : BaseTest
    {
        protected IAdminCarsService GetService(ApplicationDbContext dbContext)
        {
            var pictureService = new Mock<IAdminPicturesService>().Object;

            return this.GetService(dbContext, pictureService);
        }

        protected IAdminCarsService GetService(ApplicationDbContext dbContext, IAdminPicturesService pictureService)
        {
            var carRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var selectListItemsService = new SelectListItemsService(dbContext);
            var carsService = this.GetCarService(dbContext, carRepository);
            var service = new AdminCarsService(
                carRepository, 
                carOptionsRepository,
                pictureService, 
                selectListItemsService, 
                carsService);

            return service;
        }

        private ICarsService GetCarService(ApplicationDbContext dbContext, ICarRepository carRepository)
        {
            var userManager = CommonMockTestMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockTestMethods.GetMockedSignInManager(userManager).Object;
            var readService = new ReadService(dbContext);
            var carsService = new CarsService(signInManager, carRepository, readService);

            return carsService;
        }
    }
}
