using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class EngineRepository : BaseRepository<Engine>
    {
        public EngineRepository(DbContext dbContext) 
            : base(dbContext) { }
    }
}
