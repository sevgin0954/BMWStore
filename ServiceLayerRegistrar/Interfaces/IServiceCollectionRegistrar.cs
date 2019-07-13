using System;

namespace ServiceLayerRegistrar.Interfaces
{
    public interface IServiceCollectionRegistrar
    {
        void AddScopedServices(Type type);
    }
}
