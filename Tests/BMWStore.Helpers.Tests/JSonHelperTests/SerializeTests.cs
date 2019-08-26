using System;
using Xunit;

namespace BMWStore.Helpers.Tests.JSonHelperTests
{
    public class SerializeTests
    {
        [Fact]
        public void WithData_ShouldSerialize()
        {
            var data = Guid.NewGuid().ToString();

            var byteData = JSonHelper.Serialize(data);

            Assert.True(byteData.Length > 0);
        }
    }
}
