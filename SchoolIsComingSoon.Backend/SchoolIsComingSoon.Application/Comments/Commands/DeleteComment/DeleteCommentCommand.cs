using MediatR;

namespace SchoolIsComingSoon.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public Guid Id { get; set; }
    }
}