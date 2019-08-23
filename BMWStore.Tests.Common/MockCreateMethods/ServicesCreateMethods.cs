using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace BMWStore.Tests.Common.MockCreateMethods
{
    public static class ServicesCreateMethods
    {
        public static IDistributedCache CreateCache()
        {
            var services = new ServiceCollection();
            services.AddDistributedMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var cache = serviceProvider.GetService<IDistributedCache>();
            return cache;
        }
    }
}
