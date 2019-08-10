using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace BMWStore.Services
{
    public class CookiesService : ICookiesService
    {
        public void SetCookieValue(
            IResponseCookies responseCookies,
            string key,
            string value)
        {
            responseCookies.Append(key, value);
        }

        public TEnumValue GetValueOrDefault<TEnumValue>(
            IRequestCookieCollection requestCookies,
            string key) where TEnumValue : struct, Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnumValue));
            DataValidator.ValidateNotEmptyCollection(enumValues, ErrorConstants.EmptyEnum);

            var defaultValueEnum = (TEnumValue)enumValues.GetValue(0);
            var valueString = requestCookies[key];

            TEnumValue valueEnum;
            if (Enum.TryParse(valueString, out valueEnum) == false)
            {
                valueEnum = defaultValueEnum;
            }

            return valueEnum;
        }
    }
}