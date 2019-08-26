using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.FilterTypesServiceTests
{
    public abstract class BaseFilterTypesServiceTest : BaseTest
    {
        public IFilterTypesService GetService()
        {
            var service = new FilterTypesService();

            return service;
        }
    }
}
