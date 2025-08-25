using FluentValidation;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPost
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(post => post.Id).NotEqual(Guid.Empty);
        }
    }
}