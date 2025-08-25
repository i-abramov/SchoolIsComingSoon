using FluentValidation;

namespace SchoolIsComingSoon.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryValidator : AbstractValidator<GetCommentListQuery>
    {
        public GetCommentListQueryValidator()
        {
            RuleFor(x => x.PostId).NotEqual(Guid.Empty);
        }
    }
}