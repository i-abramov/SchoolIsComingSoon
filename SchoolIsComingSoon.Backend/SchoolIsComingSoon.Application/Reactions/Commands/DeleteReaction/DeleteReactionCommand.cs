using MediatR;

namespace SchoolIsComingSoon.Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}