﻿using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies
{
    public class ReturnAllMultipleFilterStrategy : ICarMultipleFilterStrategy
    {
        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            return cars;
        }
    }
}
