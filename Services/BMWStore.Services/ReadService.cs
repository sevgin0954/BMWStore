using BMWStore.Common.Constants;
using BMWStore.Common.Extensions;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<TModel> GetModelByIdAsync<TModel, TEntity>(string id)
            where TEntity : class
            where TModel : class
        {
            var model = await this.dbContext
               .FindAll<TEntity>(id)
               .To<TModel>()
               .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }
    }
}
