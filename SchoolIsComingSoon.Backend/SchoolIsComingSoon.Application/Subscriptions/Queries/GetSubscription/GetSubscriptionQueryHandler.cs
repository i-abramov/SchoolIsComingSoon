using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, SubscriptionVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSubscriptionQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubscriptionVm> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Subscriptions
                .ProjectTo<SubscriptionVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(subscription => subscription.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Subscription), request.Id);
            }

            return _mapper.Map<SubscriptionVm>(entity);
        }
    }
}