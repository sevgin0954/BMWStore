using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class EngineRepository : BaseRepository<Engine>, IEngineRepository
    {
        public EngineRepository(ApplicationDbContext dbContext) 
            : base(dbContext) { }
    }
}
