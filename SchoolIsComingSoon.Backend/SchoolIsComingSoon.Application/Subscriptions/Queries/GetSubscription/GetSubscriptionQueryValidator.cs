using FluentValidation;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryValidator : AbstractValidator<GetSubscriptionQuery>
    {
        public GetSubscriptionQueryValidator()
        {
            RuleFor(subscription => subscription.Id).NotEqual(Guid.Empty);
        }
    }
}