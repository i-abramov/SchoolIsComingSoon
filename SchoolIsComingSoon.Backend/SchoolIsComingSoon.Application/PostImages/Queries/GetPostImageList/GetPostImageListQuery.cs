using MediatR;

namespace SchoolIsComingSoon.Application.PostImages.Queries.GetPostImageList
{
    public class GetPostImageListQuery : IRequest<PostImageListVm>
    {
        public Guid PostId { get; set; }
    }
}