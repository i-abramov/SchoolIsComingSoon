using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.PostImages.Queries.GetPostImageList
{
    public class GetPostImageListQueryHandler : IRequestHandler<GetPostImageListQuery, PostImageListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostImageListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostImageListVm> Handle(GetPostImageListQuery request, CancellationToken cancellationToken)
        {
            var postImagesQuery = await _dbContext.Images
                .Where(postImage => postImage.PostId == request.PostId)
                .ProjectTo<PostImageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PostImageListVm { Images = postImagesQuery };
        }
    }
}