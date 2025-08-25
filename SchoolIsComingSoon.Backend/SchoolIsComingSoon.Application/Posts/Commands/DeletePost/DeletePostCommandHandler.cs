using MediatR;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public DeletePostCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            _dbContext.Posts.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}