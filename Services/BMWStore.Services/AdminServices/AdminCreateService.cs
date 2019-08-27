using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Services.AdminServices.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminCreateService : IAdminCreateService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminCreateService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync<TEntity, TModel>(TModel creatingModel)
            where TEntity : class
            where TModel : class
        {
            var dbEntity = Mapper.Map<TEntity>(creatingModel);

            this.dbContext.Set<TEntity>().Add(dbEntity);

            var rowsAffected = await this.dbContext.SaveChangesAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }
    }
}
