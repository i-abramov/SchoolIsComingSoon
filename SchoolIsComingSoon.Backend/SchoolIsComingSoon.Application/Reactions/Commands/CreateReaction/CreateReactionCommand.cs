using MediatR;

namespace SchoolIsComingSoon.Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}