using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Extensions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
    {
        private readonly ISicsDbContext _dbContext;

        public UpdatePostCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts.Include(p => p.Categories).FirstOrDefaultAsync(post =>
                post.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            entity.Text = request.Text;
            entity.EditDate = DateTime.Now.ToCommentFormat();

            entity.Categories.Clear();

            foreach (var name in request.Categories.Split("\n"))
            {
                if (name != "")
                {
                    PostCategory category = await _dbContext.Categories.FirstAsync(c => c.Name == name);
                    
                    entity.Categories.Add(category);
                    _dbContext.Categories.Attach(category);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}