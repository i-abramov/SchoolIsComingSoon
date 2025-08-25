using FluentValidation;

namespace SchoolIsComingSoon.Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandValidator : AbstractValidator<DeleteReactionCommand>
    {
        public DeleteReactionCommandValidator()
        {
            RuleFor(deleteReactionCommand => deleteReactionCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteReactionCommand => deleteReactionCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}