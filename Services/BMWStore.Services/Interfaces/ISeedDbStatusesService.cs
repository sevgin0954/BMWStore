﻿using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbStatusesService
    {
        Task SeedTestDriveStatusesAsync(params string[] statusNames);
    }
}
