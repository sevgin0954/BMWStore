using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.FilterTypesServiceTests
{
    public abstract class BaseFilterTypesServiceTest
    {
        public IFilterTypesService GetService()
        {
            var service = new FilterTypesService();

            return service;
        }
    }
}
