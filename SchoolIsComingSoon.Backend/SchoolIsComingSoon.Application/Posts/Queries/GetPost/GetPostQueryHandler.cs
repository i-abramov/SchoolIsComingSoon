using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Posts.Queries.GetPost
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostVm> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts
                .ProjectTo<PostVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(post => post.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            return _mapper.Map<PostVm>(entity);
        }
    }
}