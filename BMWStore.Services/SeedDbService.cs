using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BMWStore.Services
{
    public class SeedDbService : ISeedDbService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public SeedDbService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public void SeedAdmin(string password, string email)
        {
            if (IsUserExist(email) == false)
            {
                var dbUser = this.AddNewUser(password, email);
                this.userManager.AddToRoleAsync(dbUser, UserRoleConstants.Admin);

                this.dbContext.SaveChanges();
            }
        }

        private bool IsUserExist(string email)
        {
            return this.dbContext.Users.Any(u => u.NormalizedEmail == email);
        }

        private User AddNewUser(string password, string email)
        {
            var dbUser = new User()
            {
                UserName = email,
                Email = email
            };
            this.userManager.CreateAsync(dbUser, password).GetAwaiter().GetResult();

            this.dbContext.SaveChanges();

            return dbUser;
        }

        public void SeedRoles(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (IsRoleExist(role) == false)
                {
                    AddNewRole(role);
                }
            }

            this.dbContext.SaveChanges();
        }

        private bool IsRoleExist(string roleName)
        {
            return dbContext.Roles.Any(r => r.NormalizedName == roleName.ToUpper());
        }

        private void AddNewRole(string roleName)
        {
            var dbRole = new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            this.dbContext.Roles.Add(dbRole);
        }
    }
}
