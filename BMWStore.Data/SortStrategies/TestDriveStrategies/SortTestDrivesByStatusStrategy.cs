﻿using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.SortStrategies.TestDriveStrategies
{
    public class SortTestDrivesByStatusStrategy : ITestDriveSortStrategy
    {
        public IQueryable<TestDrive> Sort(IQueryable<TestDrive> testDrives)
        {
            var sortedTestDrives = testDrives
                .OrderBy(td => td.Status.Name == TestDriveStatus.Upcoming.ToString() ? 0 : 1)
                .ThenBy(td => td.Status.Name == TestDriveStatus.Passed.ToString() ? 0 : 1);

            return sortedTestDrives;
        }
    }
}
