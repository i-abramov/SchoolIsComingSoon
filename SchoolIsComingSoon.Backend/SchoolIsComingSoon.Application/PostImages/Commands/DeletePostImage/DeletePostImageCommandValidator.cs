using FluentValidation;

namespace SchoolIsComingSoon.Application.PostImages.Commands.DeletePostImage
{
    public class DeletePostImageCommandValidator : AbstractValidator<DeletePostImageCommand>
    {
        public DeletePostImageCommandValidator()
        {
            RuleFor(deletePostImageCommand => deletePostImageCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deletePostImageCommand => deletePostImageCommand.PostId).NotEqual(Guid.Empty);
        }
    }
}