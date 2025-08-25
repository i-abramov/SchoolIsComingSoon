using MediatR;

namespace SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}