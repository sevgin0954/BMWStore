using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace BMWStore.Services
{
    public class AdminSortCookieService : IAdminSortCookieService
    {
        public void ChangeSortDirectionCookie(IResponseCookies responseCookies, SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminUsersSortDirectionKey;
            var sortDirectionValue = sortDirection.ToString();
            responseCookies.Append(sortDirectionKey, sortDirectionValue);
        }

        public void ChangeSortTypeCookie(IResponseCookies responseCookies, UserSortStrategyType sortStrategyName)
        {
            var sortTypeKey = WebConstants.CookieAdminUsersSortTypeKey;
            var sortTypeValue = sortStrategyName.ToString();
            responseCookies.Append(sortTypeKey, sortTypeValue);
        }

        public SortStrategyDirection GetSortStrategyDirectionOrDefault(IRequestCookieCollection requestCookies)
        {
            var sortDirectionKey = WebConstants.CookieAdminUsersSortDirectionKey;
            var sortDirectionValue = requestCookies[sortDirectionKey];

            var sortDirection = SortStrategyDirection.Ascending;
            if (Enum.TryParse(sortDirectionValue, out sortDirection) == false)
            {
                sortDirection = SortStrategyDirection.Ascending;
            }

            return sortDirection;
        }

        public UserSortStrategyType GetSortStrategyTypeOrDefault(IRequestCookieCollection requestCookies)
        {
            var sortTypeKey = WebConstants.CookieAdminUsersSortTypeKey;
            var sortTypeValue = requestCookies[sortTypeKey];

            var sortType = UserSortStrategyType.Name;
            if (Enum.TryParse(sortTypeValue, out sortType) == false)
            {
                sortType = UserSortStrategyType.Name;
            }

            return sortType;
        }
    }
}
