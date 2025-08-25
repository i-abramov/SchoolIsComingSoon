using MediatR;
using SchoolIsComingSoon.Application.Extensions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly ISicsDbContext _dbContext;

        public CreateCommentCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment()
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Text = request.Text,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now.ToCommentFormat(),
                EditDate = null
            };

            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}