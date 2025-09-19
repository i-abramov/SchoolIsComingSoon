using MediatR;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQuery : IRequest<SubscriptionVm>
    {
        public Guid Id { get; set; }
    }
}