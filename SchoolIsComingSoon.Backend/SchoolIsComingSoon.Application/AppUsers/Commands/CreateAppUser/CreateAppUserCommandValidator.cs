using FluentValidation;

namespace SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(createAppUserCommand => createAppUserCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createAppUserCommand => createAppUserCommand.UserName).NotEmpty();
            RuleFor(createAppUserCommand => createAppUserCommand.FirstName).NotEmpty();
            RuleFor(createAppUserCommand => createAppUserCommand.LastName).NotEmpty();
            RuleFor(createAppUserCommand => createAppUserCommand.Email).NotEmpty();
        }
    }
}