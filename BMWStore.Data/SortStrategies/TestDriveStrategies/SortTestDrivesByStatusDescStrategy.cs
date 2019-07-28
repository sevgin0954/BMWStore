﻿using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByStatusDescStrategy : ITestDriveSortStrategy
    {
        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives
                .OrderByDescending(td => td.Status.Name == TestDriveStatus.Upcoming.ToString() ? 0 : 1)
                .ThenByDescending(td => td.Status.Name == TestDriveStatus.Passed.ToString() ? 0 : 1);

            return sortedTestDrives;
        }
    }
}
