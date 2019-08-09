using BMWStore.Services.Interfaces;
using Moq;

namespace BMWStore.Services.Tests.CarsInvertoryServiceTests
{
    public abstract class BaseCarsInvertoryServiceTest
    {
        protected ICarsInvertoryService GetService()
        {
            var carYearService = new CarYearService();
            var carSeriesService = new CarSeriesService();
            var carModelTypeService = new CarModelTypeService();
            var mockedCarPriceService = new Mock<ICarPriceService>();
            CommonMockServicesTestMethods.SetupCarPriceService(mockedCarPriceService);
            var filterTypesService = new FilterTypesService();
            var mockedCarsService = new Mock<ICarsService>();
            CommonMockServicesTestMethods.SetupCarsService(mockedCarsService);
            var service = new CarsInvertoryService(
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
