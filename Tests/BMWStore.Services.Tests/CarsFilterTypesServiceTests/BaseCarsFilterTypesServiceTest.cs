using BMWStore.Data;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.MockTestMethods;
using Moq;

namespace BMWStore.Services.Tests.CarsFilterTypesServiceTests
{
    public abstract class BaseCarsFilterTypesServiceTest : BaseTest
    {
        protected ICarsFilterTypesService GetService(ApplicationDbContext dbContext)
        {
            var carModelTypeService = new CarModelTypeService();
            var carYearService = new CarYearService();
            var carSeriesService = new CarSeriesService();
            var carPriceService = this.GetMockedCarPriceService();
            var service = new CarsFilterTypesService(
                carModelTypeService,
                carYearService,
                carSeriesService,
                carPriceService);

            return service;
        }

        private ICarPriceService GetMockedCarPriceService()
        {
            var carPriceService = new Mock<ICarPriceService>();
            CommonMockServicesMethods.SetupCarPriceService(carPriceService);

            return carPriceService.Object;
        }
    }
}
