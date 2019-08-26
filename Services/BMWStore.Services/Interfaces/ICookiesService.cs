using Microsoft.AspNetCore.Http;
using System;

namespace BMWStore.Services.Interfaces
{
    public interface ICookiesService
    {
        void SetCookieValue(
            IResponseCookies responseCookies,
            string key,
            string value);
        TEnumValue GetValueOrDefault<TEnumValue>(
            IRequestCookieCollection requestCookies,
            string sortTypeKey) where TEnumValue : struct, Enum;
    }
}
