using BMWStore.Common.Constants;
using System;
using System.Collections.Generic;

namespace BMWStore.Helpers
{
    public static class ParameterParser
    {
        public static decimal?[] ParsePriceRange(string priceRange)
        {
            var priceRanges = new decimal?[] { null, null };

            if (priceRange != null && priceRange != WebConstants.AllFilterTypeModelValue)
            {
                var priceParts = priceRange
                    .Split(" -".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (priceParts.Length == 2)
                {
                    var priceRange1 = GetParseRangeOrDefault(priceParts[0]);
                    if (priceRange1 == null)
                    {
                        return priceRanges;
                    }

                    var priceRange2 = GetParseRangeOrDefault(priceParts[1]);
                    if (priceRange2 == null)
                    {
                        return priceRanges;
                    }

                    priceRanges[0] = priceRange1;
                    priceRanges[1] = priceRange2;
                }
            }

            return priceRanges;
        }

        private static decimal? GetParseRangeOrDefault(string priceRange)
        {
            decimal parseRange;
            var isParseSuccessful = decimal.TryParse(priceRange, out parseRange);
            if (isParseSuccessful)
            {
                return parseRange;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<string> ParseSearchKeyWordsParameter(string inputStr, int minKeywordLength)
        {
            var parsedKeyWord = new List<string>();

            var inputParts = inputStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inputParts.Length; i++)
            {
                var currentKeyWord = inputParts[i].ToLower();
                if (currentKeyWord == "series")
                {
                    if (i > 0)
                    {
                        var seriesKeyWord = $"{inputParts[i - 1]} {currentKeyWord}";
                        parsedKeyWord.Add(seriesKeyWord);
                    }
                }
                else if (currentKeyWord.Length >= minKeywordLength)
                {
                    parsedKeyWord.Add(currentKeyWord);
                }
            }

            return parsedKeyWord;
        }
    }
}