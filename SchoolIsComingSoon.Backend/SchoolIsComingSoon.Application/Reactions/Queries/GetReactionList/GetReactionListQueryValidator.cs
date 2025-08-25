using FluentValidation;

namespace SchoolIsComingSoon.Application.Reactions.Queries.GetReactionList
{
    public class GetReactionListQueryValidator : AbstractValidator<GetReactionListQuery>
    {
        public GetReactionListQueryValidator()
        {
            RuleFor(x => x.PostId).NotEqual(Guid.Empty);
        }
    }
}