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
                    .Split(" -}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (priceParts.Length == 2)
                {
                    priceRanges[0] = decimal.Parse(priceParts[0]);
                    priceRanges[1] = decimal.Parse(priceParts[1]);
                }
            }

            return priceRanges;
        }
    }
}
