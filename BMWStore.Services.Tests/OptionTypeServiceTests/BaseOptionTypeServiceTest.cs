using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.OptionTypeServiceTests
{
    public abstract class BaseOptionTypeServiceTest
    {
        public IOptionTypeService GetService()
        {
            var service = new OptionTypeService();

            return service;
        }
    }
}
