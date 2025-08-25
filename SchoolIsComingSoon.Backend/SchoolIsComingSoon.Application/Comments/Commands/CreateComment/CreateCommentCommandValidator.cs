using FluentValidation;

namespace SchoolIsComingSoon.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(createCommentCommand => createCommentCommand.PostId).NotEqual(Guid.Empty);
            RuleFor(createCommentCommand => createCommentCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}