using MediatR;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile
{
    public class CreatePostFileCommandHandler : IRequestHandler<CreatePostFileCommand, Guid>
    {
        private readonly ISicsDbContext _dbContext;

        public CreatePostFileCommandHandler(ISicsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostFileCommand request,
            CancellationToken cancellationToken)
        {
            var file = new PostFile()
            {
                PostId = request.PostId,
                Name = request.Name,
                Base64Code = request.Base64Code,
                FileType = request.FileType
            };

            await _dbContext.Files.AddAsync(file, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return file.Id;
        }
    }
}