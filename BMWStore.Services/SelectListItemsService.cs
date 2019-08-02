using BMWStore.Data;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SelectListItemsService : ISelectListItemsService
    {
        private readonly ApplicationDbContext dbContext;

        public SelectListItemsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SelectItemsWithValues(IEnumerable<SelectListItem> selectListItems, params string[] values)
        {
            foreach (var selectListItem in selectListItems)
            {
                if (values.Contains(selectListItem.Value))
                {
                    selectListItem.Selected = true;
                }
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync<TEntity>() where TEntity : class
        {
            var selectListItems = await this.dbContext
                .Set<TEntity>()
                .AsQueryable()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }
    }
}
