﻿using AutoMapper;
using BMWStore.Common.Enums;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarInvertoryConciseViewModel : CarConciseViewModel, IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public bool IsTestDriveScheduled { get; set; }

        public string TestDriveId { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            var isUserSignedIn = false;
            configuration.CreateMap<BaseCar, CarInvertoryConciseViewModel>()
                .ForMember(dest => dest.TestDriveId, opt => opt.MapFrom(src => isUserSignedIn ?
                    src.TestDrives
                    .Where(td => td.Status.Name == TestDriveStatus.Upcoming.ToString())
                    .FirstOrDefault().Id
                        :
                    null
                ))
                .ForMember(dest => dest.IsTestDriveScheduled, opt => opt.MapFrom(src => src.TestDrives
                    .Any(td => td.Status.Name == TestDriveStatus.Upcoming.ToString())));

            base.CreateMappings(configuration);
        }
    }
}