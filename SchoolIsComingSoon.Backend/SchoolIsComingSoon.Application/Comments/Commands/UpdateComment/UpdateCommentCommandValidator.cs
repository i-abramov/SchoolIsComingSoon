using FluentValidation;

namespace SchoolIsComingSoon.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(updateCommentCommand => updateCommentCommand.PostId).NotEqual(Guid.Empty);
            RuleFor(updateCommentCommand => updateCommentCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateCommentCommand => updateCommentCommand.Id).NotEqual(Guid.Empty);
        }
    }
}