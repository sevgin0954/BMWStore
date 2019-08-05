using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

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

        public static User SeedUser(string roleName, ApplicationDbContext dbContext)
        {
            var user = new User();
            var role = dbContext.Roles.Where(r => r.NormalizedName == roleName.ToUpper()).First();
            user.Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = role.Id
            });

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
        }

        public static User SeedUser(ApplicationDbContext dbContext)
        {
            var user = new User();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
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

        public static Option SeedOption(ApplicationDbContext dbContext)
        {
            var dbOption = new Option();
            dbContext.Options.Add(dbOption);
            dbContext.SaveChanges();

            return dbOption;
        }

        public static Engine SeedEngine(ApplicationDbContext dbContext)
        {
            var dbEngine = new Engine();
            dbContext.Engines.Add(dbEngine);
            dbContext.SaveChanges();

            return dbEngine;
        }

        public static FuelType SeedFuelType(ApplicationDbContext dbContext)
        {
            var dbFuelType = new FuelType();
            dbContext.FuelTypes.Add(dbFuelType);
            dbContext.SaveChanges();

            return dbFuelType;
        }

        public static ModelType SeedModelType(ApplicationDbContext dbContext)
        {
            var dbModelType = new ModelType();
            dbContext.ModelTypes.Add(dbModelType);
            dbContext.SaveChanges();

            return dbModelType;
        }

        public static Series SeedSeries(ApplicationDbContext dbContext)
        {
            var dbSeries = new Series();
            dbContext.Series.Add(dbSeries);
            dbContext.SaveChanges();

            return dbSeries;
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

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCarWithEverything<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var pictures = new List<Picture>() { new Picture() { PublicId = "" } };
            var dbCar = new TCar();
            dbCar.Pictures = pictures;
            dbCar.ModelType = new ModelType();
            dbCar.Series = new Series();
            dbCar.Engine = new Engine();
            dbCar.FuelType = new FuelType();
            dbCar.Options.Add(new CarOption() { Option = new Option() });

            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCarWithTestDrive<TCar>(ApplicationDbContext dbContext, string usedId) 
            where TCar : BaseCar, new()
        {
            var dbCar = SeedCarWithEverything<TCar>(dbContext);
            var dbStatus = CommonCreateEntitiesTestMethods.CreateStatus(dbContext);
            var testDrive = new TestDrive()
            {
                Car = dbCar,
                UserId = usedId,
                Status = dbStatus
            };

            dbContext.TestDrives.Add(testDrive);
            dbContext.SaveChanges();

            return dbCar;
        }

        public static TestDrive SeedTestDriveWithCar<TCar>(
            ApplicationDbContext dbContext,
            string userId,
            TestDriveStatus status = TestDriveStatus.Upcoming)
            where TCar : BaseCar, new()
        {
            var dbCar = SeedCarWithEverything<TCar>(dbContext);
            var dbStatus = CommonCreateEntitiesTestMethods.CreateStatus(dbContext, status);
            var dbTestDrive = new TestDrive()
            {
                Car = dbCar,
                UserId = userId,
                Status = dbStatus
            };

            dbContext.TestDrives.Add(dbTestDrive);
            dbContext.SaveChanges();

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithCar<TCar>(
            ApplicationDbContext dbContext, 
            string userId,
            Status status)
            where TCar : BaseCar, new()
        {
            var dbCar = SeedCarWithEverything<TCar>(dbContext);
            var dbTestDrive = new TestDrive()
            {
                Car = dbCar,
                UserId = userId,
                Status = status
            };

            dbContext.TestDrives.Add(dbTestDrive);
            dbContext.SaveChanges();

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithUser(
            ApplicationDbContext dbContext, 
            string userId, 
            Status status)
        {
            var dbTestDrive = new TestDrive()
            {
                UserId = userId,
                Status = status
            };

            dbContext.TestDrives.Add(dbTestDrive);
            dbContext.SaveChanges();

            return dbTestDrive;
        }

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
