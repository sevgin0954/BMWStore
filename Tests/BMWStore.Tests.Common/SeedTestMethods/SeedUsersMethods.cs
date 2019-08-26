using BMWStore.Data;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Tests.Common.SeedTestMethods
{
    public static class SeedUsersMethods
    {
        public static User SeedUser(ApplicationDbContext dbContext)
        {
            var user = new User();
            SeedUser(dbContext, user);

            return user;
        }

        public static User SeedUser(ApplicationDbContext dbContext, string email)
        {
            var user = new User()
            {
                Email = email,
                NormalizedEmail = email.ToUpper()
            };
            SeedUser(dbContext, user);

            return user;
        }

        public static User SeedUser(ApplicationDbContext dbContext, IdentityRole role)
        {
            var user = new User();
            var userRole = new IdentityUserRole<string>()
            {
                RoleId = role.Id
            };
            user.Roles.Add(userRole);

            SeedUser(dbContext, user);

            return user;
        }

        public static User SeedUserWithRole(ApplicationDbContext dbContext)
        {
            var dbUserRole = SeedRolesMethods.SeedUserRole(dbContext);
            var dbUser = SeedUser(dbContext, dbUserRole);

            return dbUser;
        }

        public static User SeedAdminWithRole(ApplicationDbContext dbContext)
        {
            var dbUserRole = SeedRolesMethods.SeedAdminRole(dbContext);
            var dbUser = SeedUser(dbContext, dbUserRole);

            return dbUser;
        }

        private static void SeedUser(ApplicationDbContext dbContext, User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
    }
}
