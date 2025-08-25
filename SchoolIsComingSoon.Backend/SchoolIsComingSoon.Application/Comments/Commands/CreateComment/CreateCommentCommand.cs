using MediatR;

namespace SchoolIsComingSoon.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}