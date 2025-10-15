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

            var currentSubscription = await _dbContext.CurrentSubscriptions
                .FirstOrDefaultAsync(cs => cs.UserId == request.Id, cancellationToken);

            if (currentSubscription == null)
            {
                Subscription subscription;

                if (request.Role == "Owner" || request.Role == "Admin")
                {
                    subscription = await _dbContext.Subscriptions.FirstAsync(s => s.Name == "Максимальная");
                }
                else
                {
                    subscription = await _dbContext.Subscriptions.FirstAsync(s => s.Name == "Бесплатная");
                }

                currentSubscription = new CurrentSubscription
                {
                    Id = Guid.NewGuid(),
                    ExpiresAfter = "∞",
                    UserId = request.Id,
                    SubscriptionId = subscription.Id
                };

                await _dbContext.CurrentSubscriptions.AddAsync(currentSubscription, cancellationToken);
            }

            if (user == null)
            {
                var newUser = new AppUser
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Role = request.Role,
                    SubscriptionId = currentSubscription.Id
                };

                await _dbContext.Users.AddAsync(newUser, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}