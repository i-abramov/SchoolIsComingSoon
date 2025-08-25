using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.PostFiles.Queries.GetPostFileList
{
    public class GetPostFileListQueryHandler : IRequestHandler<GetPostFileListQuery, PostFileListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostFileListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostFileListVm> Handle(GetPostFileListQuery request, CancellationToken cancellationToken)
        {
            var postFilesQuery = await _dbContext.Files
                .Where(postFile => postFile.PostId == request.PostId)
                .ProjectTo<PostFileLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PostFileListVm { Files = postFilesQuery };
        }
    }
}