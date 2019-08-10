using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.CookieServiceTests
{
    public abstract class BaseCookieServiceTest
    {
        public ICookiesService GetService()
        {
            var service = new CookiesService();

            return service;
        }
    }
}
