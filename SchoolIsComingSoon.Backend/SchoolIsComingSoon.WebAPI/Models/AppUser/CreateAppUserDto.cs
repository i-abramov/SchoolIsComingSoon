using AutoMapper;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.AppUser
{
    public class CreateAppUserDto : IMapWith<CreateAppUserCommand>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAppUserDto, CreateAppUserCommand>()
                .ForMember(appUserCommand => appUserCommand.Id,
                opt => opt.MapFrom(appUserDto => appUserDto.Id))
                .ForMember(appUserCommand => appUserCommand.UserName,
                opt => opt.MapFrom(appUserDto => appUserDto.UserName))
                .ForMember(appUserCommand => appUserCommand.FirstName,
                opt => opt.MapFrom(appUserDto => appUserDto.FirstName))
                .ForMember(appUserCommand => appUserCommand.LastName,
                opt => opt.MapFrom(appUserDto => appUserDto.LastName))
                .ForMember(appUserCommand => appUserCommand.Email,
                opt => opt.MapFrom(appUserDto => appUserDto.Email))
                .ForMember(appUserCommand => appUserCommand.Role,
                opt => opt.MapFrom(appUserDto => appUserDto.Role));
        }
    }
}