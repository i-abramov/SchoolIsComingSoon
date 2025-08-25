using FluentValidation;
using SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile;

namespace SchoolIsComingSoon.Application.PostImages.Commands.CreatePostImage
{
    public class CreatePostImageCommandValidator : AbstractValidator<CreatePostFileCommand>
    {
        public CreatePostImageCommandValidator()
        {
            RuleFor(createPostImageCommand => createPostImageCommand.Base64Code).NotEmpty();
            RuleFor(createPostImageCommand => createPostImageCommand.FileType).NotEmpty();
            RuleFor(createPostImageCommand => createPostImageCommand.PostId).NotEqual(Guid.Empty);
        }
    }
}