using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class NewCarRepository : BaseRepository<NewCar>
    {
        public NewCarRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
