using BMWStore.Common.Constants;
using Xunit;

namespace BMWStore.Helpers.Tests.ParameterParserTests
{
    public class ParsePriceRangeTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(WebConstants.AllFilterTypeModelValue)]
        [InlineData("5-")]
        [InlineData("5")]
        [InlineData("-5")]
        [InlineData("a-5")]
        [InlineData("5-a")]
        public void WithIncorrectPriceRange_ShouldReturnNullPriceRange(string priceRange)
        {
            var result = ParameterParser.ParsePriceRange(priceRange);

            Assert.Equal(2, result.Length);
            Assert.Null(result[0]);
            Assert.Null(result[1]);
        }

        [Theory]
        [InlineData("205-1")]
        [InlineData("205 - 1")]
        public void WithCorrectPriceRange_ShouldReturnCorrectPriceRange(string priceRange)
        {
            var result = ParameterParser.ParsePriceRange(priceRange);

            Assert.Equal(2, result.Length);
            Assert.Equal(205, result[0]);
            Assert.Equal(1, result[1]);
        }
    }
}
