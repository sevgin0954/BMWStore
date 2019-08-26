using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories.Interfaces
{
    public interface ICarRepository : IRepository<BaseCar>, IBaseCarRepository<BaseCar>
    {
        Task<bool> IsType(Type type, string carId);
        DbSet<TCar> Set<TCar>() where TCar : BaseCar;
    }
}
