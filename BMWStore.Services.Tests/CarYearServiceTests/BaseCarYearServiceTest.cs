﻿using BMWStore.Data;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.CarYearServiceTests
{
    public abstract class BaseCarYearServiceTest
    {
        public ICarYearService GetService()
        {
            var service = new CarYearService();

            return service;
        }
    }
}