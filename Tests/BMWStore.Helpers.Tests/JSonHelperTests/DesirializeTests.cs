using Newtonsoft.Json;
using System;
using System.Text;
using Xunit;

namespace BMWStore.Helpers.Tests.JSonHelperTests
{
    public class DesirializeTests
    {
        [Fact]
        public void WithData_ShouldDeserializeData()
        {
            var data = Guid.NewGuid().ToString();
            var dataAsString = JsonConvert.SerializeObject(data);
            var dataAsByteArray = Encoding.Default.GetBytes(dataAsString);

            var result = JSonHelper.Desirialize<string>(dataAsByteArray);

            Assert.Equal(data, result);
        }

        [Fact]
        public void WithIncorrectTObjectType_ShouldThrowException()
        {
            var data = Guid.NewGuid().ToString();
            var dataAsString = JsonConvert.SerializeObject(data);
            var dataAsByteArray = Encoding.Default.GetBytes(dataAsString);

            Assert.ThrowsAny<Exception>(() => JSonHelper.Desirialize<int[]>(dataAsByteArray));
        }
    }
}
