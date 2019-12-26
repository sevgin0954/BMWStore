using BMWStore.Common.Constants;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.MockTestMethods;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace BMWStore.Services.Tests.CookieServiceTests
{
    public class GetValueOrDefaultTests : BaseCookieServiceTest
    {
        [Fact]
        public void WithoutCookie_ShouldReturnFirstEnum()
        {
            var service = this.GetService();
            var mockedRequestCookies = new Mock<IRequestCookieCollection>();
            var key = Guid.NewGuid().ToString();

            var enumResult = service.GetValueOrDefault<Test>(mockedRequestCookies.Object, key);

            var values = Enum.GetValues(typeof(Test));

            Assert.Equal(values.GetValue(0), enumResult);
        }

        [Fact]
        public void WithCookie_ShouldReturnCookieValue()
        {
            var service = this.GetService();
            var mockedRequestCookies = new Mock<IRequestCookieCollection>();
            var key = Guid.NewGuid().ToString();
            var value = Test.Value2;
            CommonGetMockMethods.SutupMockedRequestCookieCollection(mockedRequestCookies, key, value.ToString());

            var enumResult = service.GetValueOrDefault<Test>(mockedRequestCookies.Object, key);

            Assert.Equal(value, enumResult);
        }

        [Fact]
        public void WithEmptyEnum_ShouldThrowException()
        {
            var service = this.GetService();
            var mockedRequestCookies = new Mock<IRequestCookieCollection>();
            var key = Guid.NewGuid().ToString();

            var exception = Assert
                .Throws<InvalidOperationException>(() => service.GetValueOrDefault<Empty>(mockedRequestCookies.Object, key));
            Assert.Equal(ErrorConstants.EmptyEnum, exception.Message);
        }
    }

    public enum Test
    {
        Value1, Value2
    }

    public enum Empty { }
}
