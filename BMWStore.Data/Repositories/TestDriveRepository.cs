using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class TestDriveRepository : BaseRepository<TestDrive>, ITestDriveRepository
    {
        public TestDriveRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
