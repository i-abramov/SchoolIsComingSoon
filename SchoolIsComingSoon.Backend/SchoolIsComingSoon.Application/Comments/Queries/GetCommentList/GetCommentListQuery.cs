using MediatR;

namespace SchoolIsComingSoon.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQuery : IRequest<CommentListVm>
    {
        public Guid PostId { get; set; }
    }
}