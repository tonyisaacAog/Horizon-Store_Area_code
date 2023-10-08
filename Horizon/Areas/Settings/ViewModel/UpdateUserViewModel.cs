using AutoMapper;
using Horizon.Areas.Settings.Models;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Settings.ViewModel
{
    public class UpdateUserViewModel : IHaveCustomMappings
    {
        public UpdateUserViewModel()
        {
            UserRoles = new List<UserRoleViewModel>();
        }
        [Required]
        public string Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public List<UserRoleViewModel> UserRoles { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<UpdateUserViewModel, ApplicationUser>()
                .ForMember(x => x.UserName, s => s.MapFrom(y => y.Email))
                .ForMember(x => x.Email, s => s.MapFrom(y => y.Email));
            configuration.CreateMap<ApplicationUser, UpdateUserViewModel>();
        }
    }
}
