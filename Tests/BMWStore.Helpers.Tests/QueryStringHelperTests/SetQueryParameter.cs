using Microsoft.AspNetCore.Http;
using System;
using Xunit;

namespace BMWStore.Helpers.Tests.QueryStringHelperTests
{
    public class SetQueryParameter
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WithNullOrEmptyKey_ShouldThrowException(string key)
        {
            var queryString = QueryString.Empty;
            var value = Guid.NewGuid().ToString();

            Assert.ThrowsAny<Exception>(() => QueryStringHelper.SetQueryParameter(ref queryString, key, value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WithNullOrEmpty_ShouldThrowException(string value)
        {
            var queryString = QueryString.Empty;
            var key = Guid.NewGuid().ToString();

            Assert.ThrowsAny<Exception>(() => QueryStringHelper.SetQueryParameter(ref queryString, key, value));
        }

        [Fact]
        public void WithNonExistingKey_ShouldAddParameterToQuery()
        {
            var queryString = QueryString.Empty;
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            QueryStringHelper.SetQueryParameter(ref queryString, key, value);

            var queryParts = queryString.Value.Split("?= ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(2, queryParts.Length);
            Assert.Equal(key, queryParts[0]);
            Assert.Equal(value, queryParts[1]);
        }

        [Fact]
        public void WithExistingKey_ShouldSetParameterInQuery()
        {
            var key = Guid.NewGuid().ToString();
            var oldValue = Guid.NewGuid().ToString();
            var queryString = QueryString.Create(key, oldValue);
            var value = Guid.NewGuid().ToString();

            QueryStringHelper.SetQueryParameter(ref queryString, key, value);

            var queryParts = queryString.Value.Split("?= ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(2, queryParts.Length);
            Assert.Equal(key, queryParts[0]);
            Assert.Equal(value, queryParts[1]);
        }
    }
}
