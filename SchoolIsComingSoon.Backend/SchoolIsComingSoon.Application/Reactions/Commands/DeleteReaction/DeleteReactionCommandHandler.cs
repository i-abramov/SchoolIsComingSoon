using MediatR;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public DeleteReactionCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reactions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null ||
                entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Reaction), request.Id);
            }

            _dbContext.Reactions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}