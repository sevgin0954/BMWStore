using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICacheService
    {
        Task AddInfinityCacheAsync(object obj, string cacheKey, string cacheType);
        Task RemoveAsync(string cacheType);
        Task<TResult> GetOrDefaultAsync<TResult>(string cacheKey) where TResult : class;

    }
}
