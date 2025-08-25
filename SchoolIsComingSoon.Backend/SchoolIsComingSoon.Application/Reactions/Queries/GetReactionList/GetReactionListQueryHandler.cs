using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.Reactions.Queries.GetReactionList
{
    public class GetReactionListQueryHandler : IRequestHandler<GetReactionListQuery, ReactionListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReactionListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ReactionListVm> Handle(GetReactionListQuery request, CancellationToken cancellationToken)
        {
            var reactionsQuery = await _dbContext.Reactions
                .Where(reaction => reaction.PostId == request.PostId)
                .ProjectTo<ReactionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ReactionListVm { Reactions = reactionsQuery };
        }
    }
}