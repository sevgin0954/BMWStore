using BMWStore.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace BMWStore.Services.Interfaces
{
    public interface IAdminSortCookieService
    {
        void ChangeSortDirectionCookie(IResponseCookies responseCookies, SortStrategyDirection sortDirection);
        void ChangeSortTypeCookie(IResponseCookies responseCookies, UserSortStrategyType sortStrategyName);
        SortStrategyDirection GetSortStrategyDirectionOrDefault(IRequestCookieCollection requestCookies);
        UserSortStrategyType GetSortStrategyTypeOrDefault(IRequestCookieCollection requestCookies);
    }
}
