using BMWStore.Entities;
using BMWStore.Models;
using MappingRegistrar;
using System.Reflection;

namespace BMWStore.Services.Tests
{
    public class MapperFixture
    {
        public MapperFixture()
        {
            RegisterAutoMapperMappings.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly);
        }
    }
}
