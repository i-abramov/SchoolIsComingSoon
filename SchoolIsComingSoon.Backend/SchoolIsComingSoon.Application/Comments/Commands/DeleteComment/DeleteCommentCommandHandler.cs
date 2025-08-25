using MediatR;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public DeleteCommentCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Comments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null ||
                entity.UserId != request.UserId ||
                entity.PostId != request.PostId)
            {
                throw new NotFoundException(nameof(Comment), request.Id);
            }

            _dbContext.Comments.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}