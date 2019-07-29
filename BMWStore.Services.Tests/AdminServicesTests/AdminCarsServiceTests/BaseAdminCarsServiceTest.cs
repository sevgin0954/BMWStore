using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest
    {
        protected IAdminCarsService GetService(DbContext dbContext)
        {
            var carsRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var pictureService = new Mock<IAdminPicturesService>().Object;
            var selectListItemsService = new Mock<ISelectListItemsService>().Object;

            var service = new AdminCarsService(carsRepository, carOptionsRepository, pictureService, selectListItemsService);

            return service;
        }
    }
}
