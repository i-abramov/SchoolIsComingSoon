using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public CreateAppUserCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (user == null)
            {
                var newUser = new AppUser()
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Role = request.Role
                };

                await _dbContext.Users.AddAsync(newUser, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}