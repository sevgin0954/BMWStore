﻿using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class OptionRepository : BaseRepository<Option>, IOptionRepository
    {
        public OptionRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
