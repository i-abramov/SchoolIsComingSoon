using MediatR;

namespace SchoolIsComingSoon.Application.PostImages.Commands.CreatePostImage
{
    public class CreatePostImageCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public string Base64Code { get; set; }
        public string FileType { get; set; }
    }
}