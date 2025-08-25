using MediatR;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPost
{
    public class GetPostQuery : IRequest<PostVm>
    {
        public Guid Id { get; set; }
    }
}