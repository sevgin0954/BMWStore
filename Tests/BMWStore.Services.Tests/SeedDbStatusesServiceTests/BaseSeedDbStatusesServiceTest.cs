﻿using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.SeedDbStatusesServiceTests
{
    public abstract class BaseSeedDbStatusesServiceTest : BaseTest
    {
        public ISeedDbStatusesService GetService(ApplicationDbContext dbContext)
        {
            var statusRepository = new StatusRepository(dbContext);
            var service = new SeedDbStatusesService(statusRepository);

            return service;
        }
    }
}
