using BMWStore.Common.Helpers;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.CachedServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.CachedServices
{
    public class CachedCarsFilterTypesService : ICachedCarsFilterTypesService
    {
        private readonly IDistributedCache cache;
        private readonly ICarsFilterTypesService carsFilterTypesService;

        public CachedCarsFilterTypesService(
            IDistributedCache cache,
            ICarsFilterTypesService carsFilterTypesService)
        {
            this.cache = cache;
            this.carsFilterTypesService = carsFilterTypesService;
        }

        public async Task<CarsFilterViewModel> GetCachedCarFilterModelAsync(
            string cacheKey,
            IQueryable<BaseCar> cars,
            IQueryable<BaseCar> filteredByMultipleCars)
        {
            var cachedModelAsBytes = await this.cache.GetAsync(cacheKey);
            if (cachedModelAsBytes != null)
            {
                var model = JSonHelper.Desirialize<CarsFilterViewModel>(cachedModelAsBytes);
                return model;
            }

            var filterModel = await this.carsFilterTypesService
                .GetCarFilterModelAsync(cars, filteredByMultipleCars);

            var serielizedModelAsBytes = JSonHelper.Serialize(filterModel);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.MaxValue
            };
            _ = this.cache.SetAsync(cacheKey, serielizedModelAsBytes, options);

            return filterModel;
        }
    }
}
