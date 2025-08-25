using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Application.PostImages.Commands.CreatePostImage;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.PostImage
{
    public class CreatePostImageDto : IMapWith<CreatePostImageCommand>
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public string Base64Code { get; set; }
        [Required]
        public string FileType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostImageDto, CreatePostImageCommand>()
                .ForMember(postImageCommand => postImageCommand.PostId,
                opt => opt.MapFrom(postImageDto => postImageDto.PostId))
                .ForMember(postImageCommand => postImageCommand.Base64Code,
                opt => opt.MapFrom(postImageDto => postImageDto.Base64Code))
                .ForMember(postImageCommand => postImageCommand.FileType,
                opt => opt.MapFrom(postImageDto => postImageDto.FileType));
        }
    }
}