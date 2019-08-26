using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class ModelTypeRepository : BaseRepository<ModelType>, IModelTypeRepository
    {
        public ModelTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
