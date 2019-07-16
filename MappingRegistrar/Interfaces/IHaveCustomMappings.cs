using AutoMapper;

namespace MappingRegistrar.Interfaces
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
