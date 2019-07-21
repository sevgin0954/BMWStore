using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class NewCarRepository : BaseCarRepository<NewCar>
    {
        public NewCarRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
