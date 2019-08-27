using AutoMapper;
using Enums = BMWStore.Common.Enums;
using MappingRegistrar.Interfaces;
using System;
using BMWStore.Services.Models;

namespace BMWStore.Models.TestDriveStatusModels.ViewModels
{
    public class StatusViewModel : IMapFrom<StatusServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public Enums.TestDriveStatus Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<StatusServiceModel, StatusViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<Enums.TestDriveStatus>(src.Name)));
        }
    }
}
