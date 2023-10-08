using AutoMapper;
using Horizon.Areas.Purchases.ViewModel;
using Manufacturing.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ManufacturingInfoVM:IHaveCustomMappings
    {
        public string BatchDate { get; set; }
        public string? BatchNumber { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<ManufacturingInfoVM,ManufacturingBatch>()
                .ForMember(x => x.BatchDate, y => y.MapFrom(s => s.BatchDate.ToEgyptionDate()));
            configuration.CreateMap<ManufacturingBatch, ManufacturingInfoVM>()
                .ForMember(x => x.BatchDate, y => y.MapFrom(s => s.BatchDate.ToEgyptianDate()));
        }
    }
}
