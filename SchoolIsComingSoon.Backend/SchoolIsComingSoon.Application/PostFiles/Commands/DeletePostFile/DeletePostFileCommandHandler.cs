using MediatR;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.DeletePostFile
{
    public class DeletePostFileCommandHandler : IRequestHandler<DeletePostFileCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public DeletePostFileCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostFileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Files
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.PostId != request.PostId)
            {
                throw new NotFoundException(nameof(PostFile), request.Id);
            }

            _dbContext.Files.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}