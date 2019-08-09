using BMWStore.Common.Constants;
using BMWStore.Data;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedRolesMethods
    {
        public static IdentityRole SeedAdminRole(ApplicationDbContext dbContext)
        {
            return SeedRole(dbContext, WebConstants.AdminRoleName);
        }

        public static IdentityRole SeedUserRole(ApplicationDbContext dbContext)
        {
            return SeedRole(dbContext, WebConstants.UserRoleName);
        }

        public static IdentityRole SeedRole(ApplicationDbContext dbContext, string roleName)
        {
            var dbRole = new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            dbContext.Roles.Add(dbRole);
            dbContext.SaveChanges();

            return dbRole;
        }
    }
}
