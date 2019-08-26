using AutoMapper;
using Enums = BMWStore.Common.Enums;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Models.TestDriveStatusModels.ViewModels
{
    public class StatusViewModel : IMapFrom<Status>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public Enums.TestDriveStatus Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Status, StatusViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<Enums.TestDriveStatus>(src.Name)));
        }
    }
}
