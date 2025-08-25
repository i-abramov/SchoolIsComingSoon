using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, CommentListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCommentListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CommentListVm> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            var commentsQuery = await _dbContext.Comments
                .Where(comment => comment.PostId == request.PostId)
                .ProjectTo<CommentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CommentListVm { Comments = commentsQuery };
        }
    }
}