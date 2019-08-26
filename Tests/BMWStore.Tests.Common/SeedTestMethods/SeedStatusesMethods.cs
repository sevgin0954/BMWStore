using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Tests.Common.SeedTestMethods
{
    public static class SeedStatusesMethods
    {
        public static Status SeedStatus(ApplicationDbContext dbContext, TestDriveStatus status)
        {
            var dbStatus = new Status()
            {
                Name = status.ToString()
            };
            dbContext.Statuses.Add(dbStatus);
            dbContext.SaveChanges();

            return dbStatus;
        }
    }
}
