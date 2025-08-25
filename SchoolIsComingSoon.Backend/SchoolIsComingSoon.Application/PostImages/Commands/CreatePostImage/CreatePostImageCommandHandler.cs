using MediatR;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostImages.Commands.CreatePostImage
{
    public class CreatePostImageCommandHandler : IRequestHandler<CreatePostImageCommand, Guid>
    {
        private readonly ISicsDbContext _dbContext;

        public CreatePostImageCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostImageCommand request,
            CancellationToken cancellationToken)
        {
            var image = new PostImage()
            {
                PostId = request.PostId,
                Base64Code = request.Base64Code,
                FileType = request.FileType
            };

            await _dbContext.Images.AddAsync(image, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return image.Id;
        }
    }
}