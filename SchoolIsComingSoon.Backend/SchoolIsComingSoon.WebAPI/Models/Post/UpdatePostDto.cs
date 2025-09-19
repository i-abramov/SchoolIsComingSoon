using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Application.Posts.Commands.UpdatePost;

namespace SchoolIsComingSoon.WebAPI.Models.Post
{
    public class UpdatePostDto : IMapWith<UpdatePostCommand>
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Text { get; set; }
        public string Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePostDto, UpdatePostCommand>()
                .ForMember(postCommand => postCommand.Id,
                opt => opt.MapFrom(postDto => postDto.Id))
                .ForMember(postCommand => postCommand.SubscriptionId,
                opt => opt.MapFrom(postDto => postDto.SubscriptionId))
                .ForMember(postCommand => postCommand.Text,
                opt => opt.MapFrom(postDto => postDto.Text))
                .ForMember(postCommand => postCommand.Categories,
                opt => opt.MapFrom(postDto => postDto.Categories));
        }
    }
}