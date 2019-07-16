using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class TransmissionRepository : BaseRepository<Transmission>
    {
        public TransmissionRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
