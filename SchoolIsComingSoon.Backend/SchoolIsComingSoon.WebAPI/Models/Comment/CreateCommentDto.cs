using AutoMapper;
using SchoolIsComingSoon.Application.Comments.Commands.CreateComment;
using SchoolIsComingSoon.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.Comment
{
    public class CreateCommentDto : IMapWith<CreateCommentCommand>
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommentDto, CreateCommentCommand>()
                .ForMember(commentCommand => commentCommand.PostId,
                opt => opt.MapFrom(commentDto => commentDto.PostId))
                .ForMember(commentCommand => commentCommand.Text,
                opt => opt.MapFrom(commentDto => commentDto.Text));
        }
    }
}