using FluentValidation;

namespace SchoolIsComingSoon.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(createPostCommand => createPostCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}