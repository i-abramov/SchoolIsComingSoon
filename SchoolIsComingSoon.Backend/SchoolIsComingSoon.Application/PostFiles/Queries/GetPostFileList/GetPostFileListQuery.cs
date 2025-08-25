using MediatR;

namespace SchoolIsComingSoon.Application.PostFiles.Queries.GetPostFileList
{
    public class GetPostFileListQuery : IRequest<PostFileListVm>
    {
        public Guid PostId { get; set; }
    }
}