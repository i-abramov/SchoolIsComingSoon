using FluentValidation;

namespace SchoolIsComingSoon.Application.CurrentSubscriptions.Queries.GetCurrentSubscription
{
    public class GetCurrentSubscriptionQueryValidator : AbstractValidator<GetCurrentSubscriptionQuery>
    {
        public GetCurrentSubscriptionQueryValidator()
        {
            RuleFor(subscription => subscription.UserId).NotEqual(Guid.Empty);
        }
    }
}