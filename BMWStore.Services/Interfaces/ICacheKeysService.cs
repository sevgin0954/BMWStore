using System.Collections.Generic;

namespace BMWStore.Services.Interfaces
{
    public interface ICacheKeysService
    {
        void AddKey(string cacheType, string key);
        IEnumerable<string> GetKeys(string cacheType);
    }
}
