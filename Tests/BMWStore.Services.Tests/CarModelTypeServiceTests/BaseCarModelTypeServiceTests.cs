using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.CarModelTypeServiceTests
{
    public abstract class BaseCarModelTypeServiceTests : BaseTest
    {
        protected ICarModelTypeService GetService()
        {
            var service = new CarModelTypeService();

            return service;
        }
    }
}
