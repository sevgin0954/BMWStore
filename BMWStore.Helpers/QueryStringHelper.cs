using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Helpers
{
    public static class QueryStringHelper
    {
        public static void SetQueryParameter(ref QueryString queryString, string key, string value)
        {
            var queryKvp = QueryHelpers.ParseQuery(queryString.ToString());
            queryKvp[key] = value;

            var queryKvpAsStringKvp = queryKvp
                .SelectMany(x => x.Value, (col, val) => new KeyValuePair<string, string>(col.Key, val))
                .ToList();
            var queryBuilder = new QueryBuilder(queryKvpAsStringKvp);

            queryString = queryBuilder.ToQueryString(); ;
        }
    }
}
