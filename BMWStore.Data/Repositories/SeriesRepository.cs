using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class SeriesRepository : BaseRepository<Series>
    {
        public SeriesRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
