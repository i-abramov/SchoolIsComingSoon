using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Comments.Queries.GetCommentList
{
    public class CommentLookupDto : IMapWith<Comment>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentLookupDto>()
                .ForMember(commentDto => commentDto.Id,
                    opt => opt.MapFrom(comment => comment.Id))
                .ForMember(commentDto => commentDto.Text,
                    opt => opt.MapFrom(comment => comment.Text))
                .ForMember(commentDto => commentDto.CreationDate,
                    opt => opt.MapFrom(comment => comment.CreationDate))
                .ForMember(commentDto => commentDto.UserId,
                    opt => opt.MapFrom(comment => comment.AppUser.Id))
                .ForMember(commentDto => commentDto.FirstName,
                    opt => opt.MapFrom(comment => comment.AppUser.FirstName))
                .ForMember(commentDto => commentDto.LastName,
                    opt => opt.MapFrom(comment => comment.AppUser.LastName));
        }
    }
}