using AutoMapper;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Settings.ViewModel
{
    public class BrickViewModel //: IHaveCustomMappings
    {
        public int Id { get; set; }
        public string BrickName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }

        //public void CreateMapping(Profile configuration)
        //{
        //    configuration.CreateMap<Bricks, BrickViewModel>()
        //        .ForMember(x => x.AreaName, y => y.MapFrom(s => s.Area.AreaName));

        //    configuration.CreateMap<BrickViewModel, Bricks>();
        //}
    }
}
