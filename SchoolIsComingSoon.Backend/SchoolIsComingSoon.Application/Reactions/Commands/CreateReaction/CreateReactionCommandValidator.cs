using FluentValidation;

namespace SchoolIsComingSoon.Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandValidator : AbstractValidator<CreateReactionCommand>
    {
        public CreateReactionCommandValidator()
        {
            RuleFor(createReactionCommand => createReactionCommand.PostId).NotEqual(Guid.Empty);
            RuleFor(createReactionCommand => createReactionCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}