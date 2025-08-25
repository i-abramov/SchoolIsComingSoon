using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.PostFile
{
    public class CreatePostFileDto : IMapWith<CreatePostFileCommand>
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Base64Code { get; set; }
        [Required]
        public string FileType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostFileDto, CreatePostFileCommand>()
                .ForMember(postFileCommand => postFileCommand.PostId,
                opt => opt.MapFrom(postFileDto => postFileDto.PostId))
                .ForMember(postFileCommand => postFileCommand.Name,
                opt => opt.MapFrom(postFileDto => postFileDto.Name))
                .ForMember(postFileCommand => postFileCommand.Base64Code,
                opt => opt.MapFrom(postFileDto => postFileDto.Base64Code))
                .ForMember(postFileCommand => postFileCommand.FileType,
                opt => opt.MapFrom(postFileDto => postFileDto.FileType));
        }
    }
}