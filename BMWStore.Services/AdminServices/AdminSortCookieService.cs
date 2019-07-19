using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Common.Validation;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace BMWStore.Services.AdminServices
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

        public SortStrategyDirection GetSortStrategyDirectionOrDefault(
            IRequestCookieCollection requestCookies, 
            string sortDirectionKey)
        {
            var sortDirectionValue = requestCookies[sortDirectionKey];

            var sortDirection = SortStrategyDirection.Ascending;
            if (Enum.TryParse(sortDirectionValue, out sortDirection) == false)
            {
                sortDirection = SortStrategyDirection.Ascending;
            }

            return sortDirection;
        }

        public TStrategyType GetSortStrategyTypeOrDefault<TStrategyType>(
            IRequestCookieCollection requestCookies, 
            string sortTypeKey) where TStrategyType : struct
        {
            var enumValues = Enum.GetValues(typeof(TStrategyType));
            DataValidator.ValidateNotEmptyCollection(enumValues, ErrorConstants.EmptyEnumException);

            var defaultSortType = (TStrategyType)enumValues.GetValue(0);

            var sortType = defaultSortType;

            var sortTypeValue = requestCookies[sortTypeKey];
            if (Enum.TryParse(sortTypeValue, out sortType) == false)
            {
                sortType = defaultSortType;
            }

            return sortType;
        }
    }
}
