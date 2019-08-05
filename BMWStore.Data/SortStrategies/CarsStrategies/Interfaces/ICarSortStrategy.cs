﻿using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Data.SortStrategies.CarsStrategies.Interfaces
{
    public interface ICarSortStrategy<TCar> where TCar : BaseCar
    {
        IQueryable<TCar> Sort(IQueryable<TCar> cars);
    }
}
