using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace BMWStore.Services.Tests.CookieServiceTests
{
    public class SetCookieValueTests : BaseCookieServiceTest
    {
        [Fact]
        public void WithKeyAndValue_ShouldSetCookie()
        {
            var service = this.GetService();
            var mockedResponseCookies = new Mock<IResponseCookies>();
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            service.SetCookieValue(mockedResponseCookies.Object, key, value);

            mockedResponseCookies.Verify(rc => rc.Append(key, value), Times.Once);
        }
    }
}
