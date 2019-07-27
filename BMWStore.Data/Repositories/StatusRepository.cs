using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class StatusRepository : BaseRepository<Status>
    {
        public StatusRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
