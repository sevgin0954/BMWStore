using BMWStore.Data;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.CarYearServiceTests
{
    public abstract class BaseCarYearServiceTest : BaseTest
    {
        public ICarYearService GetService()
        {
            var service = new CarYearService();

            return service;
        }
    }
}
