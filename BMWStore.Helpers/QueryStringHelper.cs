using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Helpers
{
    public static class QueryStringHelper
    {
        public static void SetQueryParameter(ref QueryString queryString, string key, string value)
        {
            ValidateKey(key, nameof(key));
            ValidateValue(value, nameof(value));

            var queryKvp = QueryHelpers.ParseQuery(queryString.ToString());
            queryKvp[key] = value;

            var queryKvpAsStringKvp = queryKvp
                .SelectMany(x => x.Value, (col, val) => new KeyValuePair<string, string>(col.Key, val))
                .ToList();
            var queryBuilder = new QueryBuilder(queryKvpAsStringKvp);

            queryString = queryBuilder.ToQueryString();
        }

        private static void ValidateKey(string key, string paramName)
        {
            var exception = new ArgumentException(ErrorConstants.CantBeNullOrEmptyParameter, paramName);
            DataValidator.ValidateNotNullOrEmpty(key, exception);
        }

        private static void ValidateValue(string value, string paramName)
        {
            var excpetion = new ArgumentException(ErrorConstants.CantBeNullOrEmptyParameter, paramName);
            DataValidator.ValidateNotNullOrEmpty(value, excpetion);
        }
    }
}