using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest : BaseTest
    {
        protected IAdminCarsService GetService(ApplicationDbContext dbContext)
        {
            var carRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var pictureRepository = new PictureRepository(dbContext);
            var service = new AdminCarsService(
                carRepository, 
                carOptionsRepository,
                pictureRepository,
                adminDeleteService);

            return service;
        }
    }
}
