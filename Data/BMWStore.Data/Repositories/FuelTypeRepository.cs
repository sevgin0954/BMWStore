using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class FuelTypeRepository : BaseRepository<FuelType>, IFuelTypeRepository
    {
        public FuelTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
