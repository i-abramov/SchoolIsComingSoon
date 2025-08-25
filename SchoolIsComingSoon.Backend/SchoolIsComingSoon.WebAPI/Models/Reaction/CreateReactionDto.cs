using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Application.Reactions.Commands.CreateReaction;
using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.WebAPI.Models.Reaction
{
    public class CreateReactionDto : IMapWith<CreateReactionCommand>
    {
        [Required]
        public Guid PostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateReactionDto, CreateReactionCommand>()
                .ForMember(reactionCommand => reactionCommand.PostId,
                opt => opt.MapFrom(reactionDto => reactionDto.PostId));
        }
    }
}