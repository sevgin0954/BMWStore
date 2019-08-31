using BMWStore.Data;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.MockTestMethods;
using Moq;

namespace BMWStore.Services.Tests.HomeServiceTests
{
    public abstract class BaseGetSearchModelAsyncTest : BaseTest
    {

        public IHomeService GetService(ApplicationDbContext dbContext)
        {
            var carInventoriesService = new CarInventoriesService();
            var carYearService = new CarYearService();
            var carModelTypeService = new CarModelTypeService();
            var mockedCarPriceService = new Mock<ICarPriceService>();
            CommonMockServicesMethods.SetupCarPriceService(mockedCarPriceService);
            var service = new HomeService(
                carInventoriesService, 
                carYearService, 
                carModelTypeService, 
                mockedCarPriceService.Object);

            return service;
        }
    }
}
