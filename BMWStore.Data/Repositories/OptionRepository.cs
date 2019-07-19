using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class OptionRepository : BaseRepository<Option>
    {
        public OptionRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
