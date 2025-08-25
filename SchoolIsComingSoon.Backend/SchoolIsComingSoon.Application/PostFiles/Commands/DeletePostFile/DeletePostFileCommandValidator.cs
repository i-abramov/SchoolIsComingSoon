using FluentValidation;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.DeletePostFile
{
    public class DeletePostFileCommandValidator : AbstractValidator<DeletePostFileCommand>
    {
        public DeletePostFileCommandValidator()
        {
            RuleFor(deletePostFileCommand => deletePostFileCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deletePostFileCommand => deletePostFileCommand.PostId).NotEqual(Guid.Empty);
        }
    }
}