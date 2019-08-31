using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class NewCarRepository : BaseRepository<NewCar>, INewCarRepository
    {
        public NewCarRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
