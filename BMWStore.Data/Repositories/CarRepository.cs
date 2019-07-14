using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class CarRepository : BaseRepository<BaseCar>
    {
        public CarRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
