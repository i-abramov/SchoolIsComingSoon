using MediatR;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandHandler : IRequestHandler<CreateReactionCommand, Guid>
    {
        private readonly ISicsDbContext _dbContext;

        public CreateReactionCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = new Reaction()
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Id = Guid.NewGuid()
            };

            await _dbContext.Reactions.AddAsync(reaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return reaction.Id;
        }
    }
}