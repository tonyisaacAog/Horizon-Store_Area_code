using AutoMapper;
using System.ComponentModel.DataAnnotations;
using MyInfrastructure.Mapping;
using Horizon.Areas.Settings.Models;

namespace Horizon.Areas.Settings.ViewModel
{
    public class TeamViewModel : IHaveCustomMappings
    {
        public TeamViewModel()
        {
            SalesReps = new();

        }
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string TeamName { get; set; }
        [Required, StringLength(255)]
        public string TeamManager { get; set; }

        [Required, StringLength(255)]
        public string FinanceManager { get; set; }

        public List<string> SalesReps { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<TeamViewModel, Team>().ReverseMap();

        }
    }
}
