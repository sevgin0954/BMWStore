using AutoMapper;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Picture : BaseEntity, IMapFrom<string>, IHaveCustomMappings
    {
        [Required]
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        [Required]
        public string PublicId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<string, Picture>()
                .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src));
        }
    }
}