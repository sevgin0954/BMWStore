using BMWStore.Common.Constants;
using System;

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
    }
}
