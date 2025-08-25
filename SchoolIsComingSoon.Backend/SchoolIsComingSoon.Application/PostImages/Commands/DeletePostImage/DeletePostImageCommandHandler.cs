using MediatR;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostImages.Commands.DeletePostImage
{
    public class DeletePostImageCommandHandler : IRequestHandler<DeletePostImageCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public DeletePostImageCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Images
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.PostId != request.PostId)
            {
                throw new NotFoundException(nameof(PostImage), request.Id);
            }

            _dbContext.Images.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}