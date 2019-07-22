using BMWStore.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace BMWStore.Services.Interfaces
{
    public interface ISortCookieService
    {
        void ChangeSortDirectionCookie(IResponseCookies responseCookies, SortStrategyDirection sortDirection, string sortDirectionKey);
        void ChangeSortTypeCookie<TStrategyType>(IResponseCookies responseCookies, TStrategyType sortStrategyName, string sortTypeKey);
        SortStrategyDirection GetSortStrategyDirectionOrDefault(IRequestCookieCollection requestCookies, string sortDirectionKey);
        TStrategyType GetSortStrategyTypeOrDefault<TStrategyType>(
            IRequestCookieCollection requestCookies,
            string sortTypeKey) where TStrategyType : struct;
    }
}
