using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.Repositories
{
    public class TestDriveRepository : BaseRepository<TestDrive>, ITestDriveRepository
    {
        public TestDriveRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
