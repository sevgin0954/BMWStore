using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class UsedCarRepository : BaseCarRepository<UsedCar>
    {
        public UsedCarRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
