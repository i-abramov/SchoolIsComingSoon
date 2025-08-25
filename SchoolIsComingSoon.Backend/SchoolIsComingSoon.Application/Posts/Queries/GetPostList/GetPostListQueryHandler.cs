using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPostList
{
    public class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, PostListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostListVm> Handle(GetPostListQuery request, CancellationToken cancellationToken)
        {
            var postsQuery = await _dbContext.Posts
                .ProjectTo<PostLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            postsQuery.Reverse();

            return new PostListVm { Posts = postsQuery };
        }
    }
}