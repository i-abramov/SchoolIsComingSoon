using MediatR;

namespace SchoolIsComingSoon.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}