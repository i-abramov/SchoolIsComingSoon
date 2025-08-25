using FluentValidation;

namespace SchoolIsComingSoon.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(deletePostCommand => deletePostCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deletePostCommand => deletePostCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}