using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Application.Posts.Commands.CreatePost;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.Post
{
    public class CreatePostDto : IMapWith<CreatePostCommand>
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid SubscriptionId { get; set; }
        public string Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostDto, CreatePostCommand>()
                .ForMember(postCommand => postCommand.Text,
                opt => opt.MapFrom(postDto => postDto.Text))
                .ForMember(postCommand => postCommand.SubscriptionId,
                opt => opt.MapFrom(postDto => postDto.SubscriptionId))
                .ForMember(postCommand => postCommand.Categories,
                opt => opt.MapFrom(postDto => postDto.Categories));
        }
    }
}