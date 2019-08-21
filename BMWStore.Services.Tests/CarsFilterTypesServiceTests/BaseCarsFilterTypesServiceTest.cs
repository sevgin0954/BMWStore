using BMWStore.Data;
using BMWStore.Services.Interfaces;
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
            var filterTypeService = new FilterTypesService();
            var service = new CarsFilterTypesService(
                carModelTypeService,
                carYearService,
                carSeriesService,
                carPriceService,
                filterTypeService);

            return service;
        }

        private ICarPriceService GetMockedCarPriceService()
        {
            var carPriceService = new Mock<ICarPriceService>();
            CommonMockServicesTestMethods.SetupCarPriceService(carPriceService);

            return carPriceService.Object;
        }
    }
}
