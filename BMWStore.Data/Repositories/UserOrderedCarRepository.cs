using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class UserOrderedCarRepository : BaseRepository<UserOrderedCar>
    {
        public UserOrderedCarRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
