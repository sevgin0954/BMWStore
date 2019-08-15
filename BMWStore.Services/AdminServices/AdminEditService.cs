using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminEditService : IAdminEditService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminEditService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task EditAsync<TEntity, TModel>(TModel editingModel, string id)
            where TEntity : BaseEntity
            where TModel : class
        {
            var dbEntity = await this.dbContext.FindAsync<TEntity>(id);
            DataValidator.ValidateNotNull(dbEntity, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(editingModel, dbEntity);

            var rowsAffected = await this.dbContext.SaveChangesAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
