using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class OptionTypeRepository : BaseRepository<OptionType>, IOptionTypeRepository
    {
        public OptionTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
