using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, ICollection<Picture> pictures)
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar()
            {
                Pictures = pictures
            };
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, ICollection<CarOption> options) 
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar()
            {
                Options = options
            };
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, ModelType modelType) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            dbCar.ModelType = modelType;
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }
    }
}
