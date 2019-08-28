using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

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
