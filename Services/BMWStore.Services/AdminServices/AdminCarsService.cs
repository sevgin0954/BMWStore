using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using BMWStore.Services.Models;
using System.Collections.Generic;

namespace BMWStore.Services.AdminServices
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly ICarRepository carRepository;
        private readonly ICarOptionRepository carOptionRepository;
        private readonly IPictureRepository pictureRepository;
        private readonly IAdminCommonDeleteService adminDeleteService;

        public AdminCarsService(
            ICarRepository carRepository,
            ICarOptionRepository carOptionRepository,
            IPictureRepository pictureRepository,
            IAdminCommonDeleteService adminDeleteService)
        {
            this.carRepository = carRepository;
            this.carOptionRepository = carOptionRepository;
            this.pictureRepository = pictureRepository;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task CreateNewAsync<TCar>(CarServiceModel model) where TCar : BaseCar, new()
        {
            var dbCar = Mapper.Map<TCar>(model);
            dbCar.Pictures = Mapper.Map<ICollection<Picture>>(model.Pictures);

            this.carRepository.Add(dbCar);

            var rowsAffected = await this.carRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string carId)
        {
            await this.adminDeleteService.DeleteAsync<BaseCar>(carId);
        }

        public async Task EditAsync<TCar>(CarServiceModel model) where TCar : BaseCar
        {
            var dbCar = await this.carRepository.Set<TCar>().FindAsync(model.Id);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            // TODO: Optimize
            if (model.Pictures.Count() > 0)
            {
                await this.pictureRepository.RemoveRangeWhereAsync(p => p.CarId == dbCar.Id);
                dbCar.Pictures = Mapper.Map<ICollection<Picture>>(model.Pictures);
            }
            if (model.Options.Count() > 0)
            {
                await this.carOptionRepository.RemoveAllWithCarIdAsync(dbCar.Id);
            }

            Mapper.Map(model, dbCar);

            var rowsAffected = await this.carOptionRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }
    }
}
