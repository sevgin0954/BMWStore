using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using Moq;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest
    {
        protected IAdminCarsService GetService(ApplicationDbContext dbContext)
        {
            var pictureService = new Mock<IAdminPicturesService>().Object;

            return this.GetService(dbContext, pictureService);
        }

        protected IAdminCarsService GetService(ApplicationDbContext dbContext, IAdminPicturesService pictureService)
        {
            var carsRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var selectListItemsService = new SelectListItemsService(dbContext);
            var userManager = CommonMockTestMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockTestMethods.GetMockedSignInManager(userManager).Object;
            var carsService = new CarsService(signInManager);

            var service = new AdminCarsService(
                carsRepository, 
                carOptionsRepository,
                pictureService, 
                selectListItemsService, 
                carsService);

            return service;
        }
    }
}
