using BMWStore.Common.Constants;
using System;
using System.Collections;
using System.ComponentModel;

namespace BMWStore.Common.Validation
{
    public class DataValidator
    {
        public static void ValidateNotNull(object obj, Exception exception)
        {
            if (obj == null)
            {
                throw exception;
            }
        }

        public static void ValidateNotEmptyCollection(IEnumerable enumerable, string exceptionMessage)
        {
            if (enumerable.GetEnumerator().MoveNext() == false)
            {
                throw new Exception(exceptionMessage);
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
                throw new InvalidEnumArgumentException(ErrorConstants.IncorrectEnumValue);
            }
        }
    }
}
