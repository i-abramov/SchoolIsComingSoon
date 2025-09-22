using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscriptionList
{
    public class GetSubscriptionListQueryHandler : IRequestHandler<GetSubscriptionListQuery, SubscriptionListVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSubscriptionListQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubscriptionListVm> Handle(GetSubscriptionListQuery request, CancellationToken cancellationToken)
        {
            var subscriptionQuery = await _dbContext.Subscriptions
                .OrderBy(sub => sub.LVL)
                .ProjectTo<SubscriptionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SubscriptionListVm { Subscriptions = subscriptionQuery };
        }
    }
}