using AutoMapper;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Picture : IMapFrom<string>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        [Required]
        public string Url { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<string, Picture>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src));
        }
    }
}