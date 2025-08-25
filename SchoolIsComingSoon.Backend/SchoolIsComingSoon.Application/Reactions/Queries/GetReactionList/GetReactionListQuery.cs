using MediatR;

namespace SchoolIsComingSoon.Application.Reactions.Queries.GetReactionList
{
    public class GetReactionListQuery : IRequest<ReactionListVm>
    {
        public Guid PostId { get; set; }
    }
}