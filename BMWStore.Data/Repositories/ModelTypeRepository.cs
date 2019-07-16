using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class ModelTypeRepository : BaseRepository<ModelType>
    {
        public ModelTypeRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
