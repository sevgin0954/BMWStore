using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class SeriesRepository : BaseRepository<Series>, ISeriesRepository
    {
        public SeriesRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
