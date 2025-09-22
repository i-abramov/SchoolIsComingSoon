using MediatR;

namespace SchoolIsComingSoon.Application.CurrentSubscriptions.Queries.GetCurrentSubscription
{
    public class GetCurrentSubscriptionQuery : IRequest<CurrentSubscriptionVm>
    {
        public Guid UserId { get; set; }
    }
}