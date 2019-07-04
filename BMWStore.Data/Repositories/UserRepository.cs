using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}