using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockTestMethods;
using Moq;

namespace BMWStore.Services.Tests.CarsInventoryServiceTests
{
    public abstract class BaseCarsInventoryServiceTest : BaseTest
    {
        protected ICarsInventoryService GetService()
        {
            var carYearService = new CarYearService();
            var carSeriesService = new CarSeriesService();
            var carModelTypeService = new CarModelTypeService();
            var mockedCarPriceService = new Mock<ICarPriceService>();
            CommonMockServicesTestMethods.SetupCarPriceService(mockedCarPriceService);
            var filterTypesService = new FilterTypesService();
            var mockedCarsService = new Mock<ICarsService>();
            CommonMockServicesTestMethods.SetupCarsService(mockedCarsService);
            var service = new CarsInventoryService(
                carYearService, 
                carSeriesService, 
                carModelTypeService,
                mockedCarPriceService.Object,
                filterTypesService, 
                mockedCarsService.Object);

            return service;
        }
    }
}
