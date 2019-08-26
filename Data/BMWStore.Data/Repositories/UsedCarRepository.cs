using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class UsedCarRepository : BaseCarRepository<UsedCar>, IUsedCarRepository
    {
        public UsedCarRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
