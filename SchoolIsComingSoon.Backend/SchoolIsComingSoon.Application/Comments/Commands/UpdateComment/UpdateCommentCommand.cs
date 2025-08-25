using MediatR;

namespace SchoolIsComingSoon.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<Unit>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}