using System;

namespace BMWStore.Common.Validation
{
    public class DataValidator
    {
        public static void NotNullValidator(object obj, Exception exception)
        {
            if (obj == null)
            {
                throw exception;
            }
        }
    }
}
