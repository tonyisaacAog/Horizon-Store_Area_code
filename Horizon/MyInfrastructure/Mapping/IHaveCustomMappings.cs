using AutoMapper;

namespace MyInfrastructure.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMapping(Profile configuration);
    }
}
