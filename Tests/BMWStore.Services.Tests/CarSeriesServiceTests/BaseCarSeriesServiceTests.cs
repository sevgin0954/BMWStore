using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.CarSeriesServiceTests
{
    public abstract class BaseCarSeriesServiceTests : BaseTest
    {
        public ICarSeriesService GetService()
        {
            var service = new CarSeriesService();

            return service;
        }
    }
}
