using BMWStore.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSortCookieService
    {
        void ChangeSortDirectionCookie(IResponseCookies responseCookies, SortStrategyDirection sortDirection, string sortDirectionKey);
        void ChangeSortTypeCookie<TStrategyType>(IResponseCookies responseCookies, TStrategyType sortStrategyName, string sortTypeKey);
        SortStrategyDirection GetSortStrategyDirectionOrDefault(IRequestCookieCollection requestCookies, string sortDirectionKey);
        TStrategyType GetSortStrategyTypeOrDefault<TStrategyType>(
            IRequestCookieCollection requestCookies,
            string sortTypeKey) where TStrategyType : struct;
    }
}
