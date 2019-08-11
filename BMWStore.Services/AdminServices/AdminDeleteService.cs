using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminDeleteService : IAdminDeleteService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminDeleteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAsync<TEntity>(string id) where TEntity : class
        {
            this.ValidateGenericType(typeof(TEntity));

            var entity = await this.dbContext.Set<TEntity>()
                .FindAsync(id);
            DataValidator.ValidateNotNull(entity, new ArgumentException(ErrorConstants.IncorrectId));

            this.dbContext.Remove(entity);

            var rowsAffected = await this.dbContext.SaveChangesAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private void ValidateGenericType(Type type)
        {
            if (type.FullName == typeof(User).FullName)
            {
                throw new NotSupportedException(ErrorConstants.IncorrectGenericType);
            }
        }
    }
}
