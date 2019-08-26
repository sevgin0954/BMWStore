using BMWStore.Services.Interfaces;

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
