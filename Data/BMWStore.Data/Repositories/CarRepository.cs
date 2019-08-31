using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class CarRepository : BaseRepository<BaseCar>, ICarRepository
    {
        private readonly DbContext dbContext;

        public CarRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> IsType(Type type, string carId)
        {
            var isType = await this.GetAll().AnyAsync(c => c is NewCar && c.Id == carId);

            return isType;
        }

        public DbSet<TCar> Set<TCar>() where TCar : BaseCar
        {
            return this.dbContext.Set<TCar>();
        }
    }
}
