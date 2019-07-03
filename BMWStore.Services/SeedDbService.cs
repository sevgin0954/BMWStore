using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task SeedAdminAsync(string password, string email)
        {
            var dbUser = await this.CreateUser(password, email);
            await this.AddRoleToUser(dbUser, UserRoleConstants.Admin);

            await this.dbContext.SaveChangesAsync();
        }

        private async Task<User> CreateUser(string password, string email)
        {
            var dbUser = this.dbContext.Users
                .Where(u => u.NormalizedEmail == email.ToUpper())
                .FirstOrDefault();

            if (dbUser == null)
            {
                dbUser = new User()
                {
                    UserName = email,
                    Email = email
                };
                await this.AddNewUserAsync(dbUser, password);
            }

            return dbUser;
        }

        private async Task AddRoleToUser(User user, string roleName)
        {
            if (await this.userManager.IsInRoleAsync(user, UserRoleConstants.Admin) == false)
            {
                await this.userManager.AddToRoleAsync(user, UserRoleConstants.Admin);
            }
        }

        private async Task AddNewUserAsync(User user, string password)
        {
            await this.userManager.CreateAsync(user, password);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task SeedRolesAsync(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (IsRoleExist(role) == false)
                {
                    AddNewRole(role);
                }
            }

            await this.dbContext.SaveChangesAsync();
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
