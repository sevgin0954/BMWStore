using BMWStore.Common.Constants;
using System;
using System.Collections;
using System.Globalization;

namespace BMWStore.Common.Validation
{
    public class DataValidator
    {
        // TODO: Inconsistent parameters
        public static void ValidateNotNull(object obj, Exception exception)
        {
            if (obj == null)
            {
                throw exception;
            }
        }

        // TODO: Inconsistent parameters
        public static void ValidateNotEmptyCollection(IEnumerable enumerable, string exceptionMessage)
        {
            if (enumerable.GetEnumerator().MoveNext() == false)
            {
                throw new InvalidOperationException(exceptionMessage);
            }
        }

        public static void ValidateEnumType(Type type)
        {
            if (type.IsEnum == false)
            {
                throw new ArgumentException(ErrorConstants.TypeWasNotEnum);
            }
        }

        public static void ValidateNotEmptyEnum(Type enumType, string exceptionMessage)
        {
            var enumValues = Enum.GetValues(enumType);
            ValidateNotEmptyCollection(enumValues, exceptionMessage);
        }

        public static void ValidateEnumValue(string enumValue, Type enumType)
        {
            var result = new object();
            if (Enum.TryParse(enumType, enumValue, out result) == false)
            {
                throw new ArgumentException(ErrorConstants.IncorrectEnumValue);
            }
        }

        public static void ValidateYearString(string year)
        {
            var result = DateTime.UtcNow;
            var isSuccessful = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
        }

        public static void ValidateMinPrice(decimal price, string minPrice)
        {
            if (price < decimal.Parse(minPrice))
            {
                throw new Exception(ErrorConstants.IncorrectPriceRange);
            }
        }

        public static void ValidateMaxPrice(decimal price, string maxPrice)
        {
            if (price > decimal.Parse(maxPrice))
            {
                throw new Exception(ErrorConstants.IncorrectPriceRange);
            }
        }
    }
}
