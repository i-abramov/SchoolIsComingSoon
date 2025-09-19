using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPost
{
    public class PostVm : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostVm>()
                .ForMember(postVm => postVm.Text,
                    opt => opt.MapFrom(post => post.Text))
                .ForMember(postVm => postVm.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(postVm => postVm.SubscriptionId,
                    opt => opt.MapFrom(post => post.SubscriptionId))
                .ForMember(postVm => postVm.CreationDate,
                    opt => opt.MapFrom(post => post.CreationDate))
                .ForMember(postVm => postVm.EditDate,
                    opt => opt.MapFrom(post => post.EditDate))
                .ForMember(postVm => postVm.FirstName,
                    opt => opt.MapFrom(post => post.AppUser.FirstName))
                .ForMember(postVm => postVm.LastName,
                    opt => opt.MapFrom(post => post.AppUser.LastName))
                .ForMember(postVm => postVm.Categories,
                    opt => opt.MapFrom(post => post.Categories.Select(c => c.Name)));
        }
    }
}