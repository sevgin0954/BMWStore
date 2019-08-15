using BMWStore.Common.Constants;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class ReadService : IReadService
    {
        private readonly ApplicationDbContext dbContext;

        public ReadService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>()
            where TEntity : class
            where TModel : class
        {
            var models = await this.dbContext.Set<TEntity>()
                .AsQueryable()
                .To<TModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>(int pageNumber)
            where TEntity : class
            where TModel : class
        {
            var models = await this.dbContext.Set<TEntity>()
                .AsQueryable()
                .GetFromPage(pageNumber)
                .To<TModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel, TEntity>(IQueryable<TEntity> entities, int pageNumber)
            where TEntity : class
            where TModel : class
        {
            var models = await entities
                .AsQueryable()
                .GetFromPage(pageNumber)
                .To<TModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<TModel> GetModelByIdAsync<TModel, TEntity>(string id)
            where TEntity : BaseEntity
            where TModel : class
        {
            var model = await this.dbContext.Set<TEntity>()
               .AsQueryable()
               .Where(e => e.Id == id)
               .To<TModel>()
               .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }
    }
}
