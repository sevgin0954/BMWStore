using BMWStore.Common.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<int> CountTotalPagesCountAsync<TEntity>(IQueryable<TEntity> entities) where TEntity : class
        {
            var totalCarsCount = await entities.CountAsync();
            var totalPagesCount = Math.Ceiling((double)totalCarsCount / WebConstants.PageSize);

            return (int)totalPagesCount;
        }

        public static IQueryable<TEntity> GetFromPage<TEntity>(
            this IQueryable<TEntity> entities,
            int pageNumber,
            int pageSize)
            where TEntity : class
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, pageSize);

            return entities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
