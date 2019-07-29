using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class UsedCarRepository : BaseCarRepository<UsedCar>, IUsedCarRepository
    {
        public UsedCarRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
