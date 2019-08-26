using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class TransmissionRepository : BaseRepository<Transmission>, ITransmissionRepository
    {
        public TransmissionRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
