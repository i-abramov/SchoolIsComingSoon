using MediatR;

namespace SchoolIsComingSoon.Application.PostImages.Commands.DeletePostImage
{
    public class DeletePostImageCommand : IRequest<Unit>
    {
        public Guid PostId { get; set; }
        public Guid Id { get; set; }
    }
}