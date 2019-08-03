using BMWStore.Common.Constants;
using BMWStore.Data;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests
{
    public static class CommonSeedTestMethods
    {
        public static void SeedRoles(ApplicationDbContext dbContext)
        {
            SeedAdminRole(dbContext);
            SeedUserRole(dbContext);
            dbContext.SaveChanges();
        }

        public static IdentityRole SeedAdminRole(ApplicationDbContext dbContext)
        {
            var adminRole = new IdentityRole()
            {
                Name = WebConstants.AdminRoleName,
                NormalizedName = WebConstants.AdminRoleName.ToUpper()
            };
            dbContext.Roles.Add(adminRole);

            dbContext.SaveChanges();

            return adminRole;
        }

        public static IdentityRole SeedUserRole(ApplicationDbContext dbContext)
        {
            var userRole = new IdentityRole()
            {
                Name = WebConstants.UserRoleName,
                NormalizedName = WebConstants.UserRoleName.ToUpper()
            };
            dbContext.Roles.Add(userRole);

            dbContext.SaveChanges();

            return userRole;
        }
    }
}
