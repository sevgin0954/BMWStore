using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockTestMethods;
using Moq;

namespace BMWStore.Services.Tests.HomeServiceTests
{
    public abstract class BaseGetSearchModelAsyncTest : BaseTest
    {

        public IHomeService GetService(ApplicationDbContext dbContext)
        {
            var carRepository = new CarRepository(dbContext);
            var carInventoriesService = new CarInventoriesService();
            var carYearService = new CarYearService();
            var carModelTypeService = new CarModelTypeService();
            var mockedCarPriceService = new Mock<ICarPriceService>();
            CommonMockServicesTestMethods.SetupCarPriceService(mockedCarPriceService);
            var service = new HomeService(
                carRepository, 
                carInventoriesService, 
                carYearService, 
                carModelTypeService, 
                mockedCarPriceService.Object);

            return service;
        }
    }
}
