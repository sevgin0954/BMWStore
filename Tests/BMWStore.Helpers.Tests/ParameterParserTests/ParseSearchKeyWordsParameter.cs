using System;
using System.Linq;
using Xunit;

namespace BMWStore.Helpers.Tests.ParameterParserTests
{
    public class ParseSearchKeyWordsParameter
    {
        [Fact]
        public void WithNullInputStr_ShouldThrowException()
        {
            var minKeywordLength = 1;

            Assert.Throws<NullReferenceException>(() => 
                ParameterParser.ParseSearchKeyWordsParameter(null, minKeywordLength));
        }

        [Fact]
        public void WithEmptyInputStr_ShouldReturnEmptyCollection()
        {
            var keyInputStr = "";
            var minKeywordLength = 1;

            var result = ParameterParser.ParseSearchKeyWordsParameter(keyInputStr, minKeywordLength);

            Assert.Empty(result);
        }

        [Fact]
        public void WithKeyWords_ShouldFilterKeyWordsByMinKeywordLength()
        {
            var keyWordWithAboveMinLength = "333";
            var keyInputStr = $"1 22 22 {keyWordWithAboveMinLength}";
            var minKeywordLength = 3;

            var result = ParameterParser.ParseSearchKeyWordsParameter(keyInputStr, minKeywordLength);

            Assert.Single(result);
            Assert.Equal(keyWordWithAboveMinLength, result.First());
        }

        [Fact]
        public void WithKeyWordAndSeriesKeyWord_ShouldReturnCombineSeriesWithKeyWordBehind()
        {
            var keyInputStr = "3 series";
            var minKeywordLength = 2;

            var result = ParameterParser.ParseSearchKeyWordsParameter(keyInputStr, minKeywordLength);

            Assert.Single(result);
            Assert.Equal(keyInputStr, result.First());
        }
    }
}
