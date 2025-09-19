using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Extensions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly ISicsDbContext _dbContext;

        public CreatePostCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = new Post()
            {
                UserId = request.UserId,
                SubscriptionId = request.SubscriptionId,
                Text = request.Text,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now.ToPostFormat(),
                EditDate = null
            };

            foreach (var name in request.Categories.Split("\n"))
            {
                if (name != "")
                {
                    PostCategory category = await _dbContext.Categories.FirstAsync(c => c.Name == name);
                    post.Categories.Add(category);
                }
            }

            await _dbContext.Posts.AddAsync(post, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}