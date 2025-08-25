using FluentValidation;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile
{
    public class CreatePostFileCommandValidator : AbstractValidator<CreatePostFileCommand>
    {
        public CreatePostFileCommandValidator()
        {
            RuleFor(createPostFileCommand => createPostFileCommand.Name).NotEmpty();
            RuleFor(createPostFileCommand => createPostFileCommand.Base64Code).NotEmpty();
            RuleFor(createPostFileCommand => createPostFileCommand.FileType).NotEmpty();
            RuleFor(createPostFileCommand => createPostFileCommand.PostId).NotEqual(Guid.Empty);
        }
    }
}