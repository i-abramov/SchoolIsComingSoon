using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Reactions.Queries.GetReactionList
{
    public class ReactionLookupDto : IMapWith<Reaction>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reaction, ReactionLookupDto>()
                .ForMember(reactionDto => reactionDto.Id,
                    opt => opt.MapFrom(reaction => reaction.Id))
                .ForMember(reactionDto => reactionDto.UserId,
                    opt => opt.MapFrom(reaction => reaction.UserId));
        }
    }
}