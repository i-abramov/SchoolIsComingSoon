using FluentValidation;

namespace SchoolIsComingSoon.Application.PostFiles.Queries.GetPostFileList
{
    public class GetPostFileListQueryValidator : AbstractValidator<GetPostFileListQuery>
    {
        public GetPostFileListQueryValidator()
        {
            RuleFor(x => x.PostId).NotEqual(Guid.Empty);
        }
    }
}