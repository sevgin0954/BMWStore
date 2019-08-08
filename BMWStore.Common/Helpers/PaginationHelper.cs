using BMWStore.Common.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Common.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<int> CalculateTotalPagesCount<TEntity>(IQueryable<TEntity> entities) where TEntity : class
        {
            var totalCarsCount = await entities.CountAsync();
            var totalPagesCount = Math.Ceiling((double)totalCarsCount / WebConstants.PageSize);

            return (int)totalPagesCount;
        }

        public static IQueryable<TEntity> GetFromPage<TEntity>(this IQueryable<TEntity> entities, int pageNumber)
            where TEntity : class
        {
            pageNumber = Math.Max(1, pageNumber);

            return entities
                .Skip((pageNumber - 1) * WebConstants.PageSize)
                .Take(WebConstants.PageSize);
        }
    }
}
