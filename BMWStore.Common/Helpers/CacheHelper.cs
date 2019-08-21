using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Common.Helpers
{
    public static class CacheHelper
    {
        public static async Task RefreshCacheAsync(IEnumerable<string> keys, IDistributedCache cache)
        {
            foreach (var key in keys)
            {
                await cache.RefreshAsync(key);
            }
        }
    }
}
