using FluentValidation;

namespace SchoolIsComingSoon.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(updatePostCommand => updatePostCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updatePostCommand => updatePostCommand.Id).NotEqual(Guid.Empty);
        }
    }
}