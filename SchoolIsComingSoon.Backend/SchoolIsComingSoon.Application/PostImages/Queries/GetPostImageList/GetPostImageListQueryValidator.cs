using FluentValidation;

namespace SchoolIsComingSoon.Application.PostImages.Queries.GetPostImageList
{
    public class GetPostImageListQueryValidator : AbstractValidator<GetPostImageListQuery>
    {
        public GetPostImageListQueryValidator()
        {
            RuleFor(x => x.PostId).NotEqual(Guid.Empty);
        }
    }
}
