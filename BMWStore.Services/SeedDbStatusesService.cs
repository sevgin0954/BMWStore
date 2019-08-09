using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SeedDbStatusesService : ISeedDbStatusesService
    {
        private readonly IStatusRepository statusRepository;

        public SeedDbStatusesService(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public async Task SeedTestDriveStatusesAsync(params string[] statusNames)
        {
            foreach (var statusName in statusNames)
            {
                if (await this.IsStatusExistAsync(statusName) == false)
                {
                    var dbStatus = new Status()
                    {
                        Name = statusName
                    };
                    this.statusRepository.Add(dbStatus);
                }
            }

            await this.statusRepository.CompleteAsync();
        }

        private async Task<bool> IsStatusExistAsync(string statusName)
        {
            var isExist = await this.statusRepository.AnyAsync(tds => tds.Name == statusName);

            return isExist;
        }
    }
}
