using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Extensions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public UpdateCommentCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Comments.FirstOrDefaultAsync(comment =>
                comment.Id == request.Id, cancellationToken);

            var post = await _dbContext.Posts.FirstOrDefaultAsync(post =>
                post.Id == request.PostId, cancellationToken);

            if (post == null || post.Id != request.PostId ||
                entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Comment), request.Id);
            }

            entity.Text = request.Text;
            entity.EditDate = DateTime.Now.ToCommentFormat();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}