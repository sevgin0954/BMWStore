using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class FuelTypeRepository : BaseRepository<FuelType>
    {
        public FuelTypeRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
