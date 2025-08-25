using MediatR;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile
{
    public class CreatePostFileCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public string Name { get; set; }
        public string Base64Code { get; set; }
        public string FileType { get; set; }
    }
}