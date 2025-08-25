using MediatR;

namespace SchoolIsComingSoon.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string Categories { get; set; }
    }
}