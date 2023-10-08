using AutoMapper;
using Horizon.Areas.Settings.Models;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Settings.ViewModel
{
    public class CreateUserViewModel : IHaveCustomMappings
    {
        public CreateUserViewModel()
        {
            UserRoles = new List<UserRoleViewModel>();
        }
        public string? Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Mobile { get; set; }
       
        public List<UserRoleViewModel> UserRoles { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration
                .CreateMap<CreateUserViewModel, ApplicationUser>()
                .ForMember(x => x.UserName, s => s.MapFrom(y => y.UserName))
                .ForMember(x => x.Email, s => s.MapFrom(y => y.Email))
                .ForMember(x => x.PhoneNumber, s => s.MapFrom(y => y.Phone));

            configuration.CreateMap<ApplicationUser, CreateUserViewModel>();

        }
    }
}
