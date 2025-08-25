using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPostList
{
    public class PostLookupDto : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostLookupDto>()
                .ForMember(postDto => postDto.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(postDto => postDto.Text,
                    opt => opt.MapFrom(post => post.Text))
                .ForMember(postDto => postDto.CreationDate,
                    opt => opt.MapFrom(post => post.CreationDate))
                .ForMember(postVm => postVm.EditDate,
                    opt => opt.MapFrom(post => post.EditDate))
                .ForMember(postDto => postDto.FirstName,
                    opt => opt.MapFrom(post => post.AppUser.FirstName))
                .ForMember(postDto => postDto.LastName,
                    opt => opt.MapFrom(post => post.AppUser.LastName))
                .ForMember(postDto => postDto.Categories,
                    opt => opt.MapFrom(post => post.Categories.Select(c => c.Name)));
        }
    }
}